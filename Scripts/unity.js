var container = document.querySelector("#unity-container");
var canvas = document.querySelector("#unity-canvas");

var loadingBar = document.querySelector("#unity-loading-bar");
var progressBarFull = document.querySelector("#unity-progress-bar-full");
var fullscreenButton = document.querySelector("#unity-fullscreen-button");
var warningBanner = document.querySelector("#unity-warning");

var loadButton = document.querySelector("#unity-load-button");
var quitButton = document.querySelector("#unity-quit-button");

// Shows a temporary message banner/ribbon for a few seconds, or
// a permanent error message on top of the canvas if type=='error'.
// If type=='warning', a yellow highlight color is used.
// Modify or remove this function to customize the visually presented
// way that non-critical warnings and error messages are presented to the
// user.
function unityShowBanner(msg, type) {
    function updateBannerVisibility() {
        warningBanner.style.display = warningBanner.children.length ? 'block' : 'none';
    }
    var div = document.createElement('div');
    div.innerHTML = msg;
    warningBanner.appendChild(div);
    if (type == 'error') div.style = 'background: red; padding: 10px;';
    else {
        if (type == 'warning') div.style = 'background: yellow; padding: 10px;';
        setTimeout(function () {
            warningBanner.removeChild(div);
            updateBannerVisibility();
        }, 5000);
    }
    updateBannerVisibility();
}

var buildUrl = "Build";
var loaderUrl = buildUrl + "/cinema-0451.loader.js";
var config = {
    dataUrl: buildUrl + "/cinema-0451.data",
    frameworkUrl: buildUrl + "/cinema-0451.framework.js",
    codeUrl: buildUrl + "/cinema-0451.wasm",
    streamingAssetsUrl: "StreamingAssets",
    companyName: "Aps",
    productName: "Cinema0451",
    productVersion: "1",
    showBanner: unityShowBanner,
};

// By default, Unity keeps WebGL canvas render target size matched with
// the DOM size of the canvas element (scaled by window.devicePixelRatio)
// Set this to false if you want to decouple this synchronization from
// happening inside the engine, and you would instead like to size up
// the canvas DOM size and WebGL render target sizes yourself.
// config.matchWebGLToCanvasSize = false;

if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
    // Mobile device style: fill the whole browser client area with the game canvas:

    var meta = document.createElement('meta');
    meta.name = 'viewport';
    meta.content = 'width=device-width, height=device-height, initial-scale=1.0, user-scalable=no, shrink-to-fit=yes';
    document.getElementsByTagName('head')[0].appendChild(meta);
    container.className = "unity-mobile";
    canvas.className = "unity-mobile";

    // To lower canvas resolution on mobile devices to gain some
    // performance, uncomment the following line:
    // config.devicePixelRatio = 1;
}

var script = document.createElement("script");
script.src = loaderUrl;

// =========== START ON PAGE LOAD ===========

//   script.onload = () => {
//     createUnityInstance(canvas, config, (progress) => {
//       progressBarFull.style.width = 100 * progress + "%";
//           }).then((unityInstance) => {
//             loadingBar.style.display = "none";
//             fullscreenButton.onclick = () => {
//               unityInstance.SetFullscreen(1);
//             };
//           }).catch((message) => {
//             alert(message);
//           });
//         };


loadButton.addEventListener("click", function () {
    loadingBar.style.display = "block";

    loadButton.style.display = "none";

    // ===== Create Unity Instance ===== //
    createUnityInstance(canvas, config, (progress) => {
        progressBarFull.style.width = 100 * progress + "%";
    }).then((unityInstance) => {
        loadingBar.style.display = "none";

        // ===== Fullscreen ===== //
        fullscreenButton.onclick = () => {
            unityInstance.SetFullscreen(1);
        };

        // ===== Exit Unity Instance ===== //
        quitButton.onclick = () => {
            unityInstance.Quit().then(function() {
                console.log("Exiting unity instance");
            });
            // Doesn't trigger garbage collector for some reason
            unityInstance = null;

            // Reset canvas
            const context = canvas.getContext('2d');
            context.clearRect(0, 0, canvas.width, canvas.height);
            canvas.style.backgroundColor = "black";
        };

    // ===== Error Handling ===== //
    }).catch((message) => {
        alert(message);
    });
});

document.body.appendChild(script);