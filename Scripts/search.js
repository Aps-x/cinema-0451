const search_input = document.getElementById('input');
const search_button = document.getElementById('search');
const results_container = document.getElementById('results');
const results_loader = document.getElementById('results_loader');

const MEDIA_API_URL = 'https://media.nfsacollection.net/';
const COLLECTION_API_URL = 'https://api.collection.nfsa.gov.au/search?limit=25&hasMedia=yes&query=';

const MAX_ITERATION_FAILSAFE = 200;
const MAX_RESULTS_PER_CALL = 25;
const MAX_PAGES_FOR_SEARCH = 10;

let results = [];
let current_page = 1;
let total = 0;
let search_string = '';

search_button.addEventListener("click", (event) => {
    event.preventDefault();

    search_string = search_input.value;
    results_container.innerHTML = '';
    results_loader.style.display = 'grid';

    current_page = 1;  
    results = [];    
    total = 0;   

    fetchData();      
});

async function fetchData() {
    let is_more_pages = true;
    let iteration_count = 0;

    try {
        while (is_more_pages && iteration_count < MAX_ITERATION_FAILSAFE) {
            iteration_count++;

            let queryString = `${COLLECTION_API_URL}${search_string}&page=${current_page}`;
            console.log('API call:', queryString);

            // This is slower but the API was rate limiting me :(
            const response = await fetch(queryString);
            const res = await response.json();

            if (!res || !res.results || res.results.length === 0) {
                console.log('No results');
                is_more_pages = false;
                break;
            }

            // Merge new data and results
            results = results.concat(res.results);
            total = res.meta.count.total;

            // Check if more pages need to be fetched
            if (current_page * MAX_RESULTS_PER_CALL < total && current_page < MAX_PAGES_FOR_SEARCH) {
                current_page++;
            } 
            else {
                is_more_pages = false;
            }
        }

        if (results.length > 0) {
            displayResults();
        } 
        else {
            console.log('No results found');
        }
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

function displayResults() {
    results_loader.style.display = 'none';

    results.forEach(item => {
        if (item.preview && item.preview[0]) {
            const result_item = document.createElement('li');

            const img = document.createElement('img');
            img.src = MEDIA_API_URL + item.preview[0].filePath;
            img.alt = item.name;
            img.title = item.name;

            result_item.appendChild(img);
            results_container.appendChild(result_item);
        }
    });
}