# Project 2: Web App Development 
Website Link: https://aps-x.github.io/cinema-0451/

## Rationale

My web application is an exhibit called ‘Cinema 0451’ which features a handful of curated collections from the NFSA with a focus on vintage cinema. Cinema 0451 consists of two parts: the webpage and the virtual exhibit.

### The Webpage

Art Deco was an art movement that became popular in the 1920s and 1930s, coinciding with the popularization of cinema. The design of art deco reflected the luxury, modernity, and faith in technological progress of the time and featured streamlined forms, stylized patterns, bold colors, and luxurious materials. Art deco and cinema share an intertwined history as the movement influenced the aesthetics of film production as well as the architecture and decoration of movie theaters.
The overall theme of the Cinema 0451 webpage is inspired by the art deco style to compliment the vintage cinema content.
\
\
**Typography:** The webpage features three fonts:
Limelight - used for headings
Poppins - used for text
Poiret One - used as an accent font for buttons

My design features the fairly standard visual hierarchy choice of having a more decorative font for headings to stand out and convey more personality, and a more legible and easy to read font for the body text.
Limelight was chosen after I simply googled “Art deco google fonts”. Poppins was chosen because I was trying to find a simple sans-serif font similar to the one used in this reference image:
![maxresdefault](https://github.com/user-attachments/assets/09801e6a-b5b2-415e-a915-b2fc8d206a64)

\
Poiret One was chosen because I wanted to give the buttons more visual interest but Limelight was a bit too extreme.

**Colors:** The color palette is kept very simple, with just black, white, and gold being used. Gold was chosen for being a very opulent art deco color. I did experiment with making the title of the webpage red with a white outline and neon glow, similar to the reference image above. However, it looked very tacky with the Limelight font and the website was looking tacky enough.

**Imagery:** The background video at the top of the page is from Steam, which I stumbled upon by chance on someone's profile. I felt like both the pattern and the red and gold patterns matched the art deco style and the curtains looked like they would belong in a theater. The webpage also features a more typical gold shell pattern that fades in from the top, which I found on google images.

### The Virtual Exhibit

The virtual exhibit is the centerpiece of this project and was meant to provide the user with a more interactive and interesting way of exploring the NFSA’s collection. I roughly based the layout on HOYTS Belconnen and I imagined that this was an old cinema that has been turned into an exhibit for classic films.

I made the virtual exhibit using Unity which is able to compile to WebGL and Web Assembly. I chose Unity because I was familiar with it and it had an easy to use editor for creating a 3D environment. In retrospect, it might have been better to use Three.js and Blender because Unity’s Probuilder extension was awful.

I chose what I wanted to do for the exhibit before information on the NFSA api was released, so I did not realize how extremely limited the api was before starting work. When I realized that none of the items from the curated collections were available, I uploaded the items I had chosen to Firebase to meet the dynamic data requirements. I think that having the item data be separate from the unity project is a great idea for a few reasons:
1. You don’t have to duplicate the collection which could lead to inconsistencies
2. The unity player doesn’t have to load all assets before starting, which improves the time to interact considerably
3. You can change what items are shown in the exhibit without re-compiling 

Note: The unity code is compiled to web assembly, but you can verify that the images, text, and videos are downloaded dynamically with the network tab in the developer console.
The code for fetching the images and text currently allows for someone to select from a dropdown (Within the Unity editor) whether the request should be sent to the Firebase api or the NFSA api. It should also be noted that I wasted a lot of time on setting up a proxy server for bypassing CORS using Google Cloud Functions, which is why the NFSA api in the C# scripts are different.

### References

Environment asset pack (models and textures): [elbolilloduro](https://elbolilloduro.itch.io/laundry)
\
Carpet shader code: [Garret Gunnell (Acerola)](https://github.com/GarrettGunnell/Shell-Texturing/tree/main/Assets)
\
Carpet technique first seen here: [Blendo Games](https://blendogames.com/news/post/2018-11-08-carpet_tech/)
\
Lobby music: [Valve Studio Orchestra](https://www.youtube.com/watch?v=Q7eJg7hRvqE)
\
Border Image Trick: [Smashing Magazine](https://www.smashingmagazine.com/2024/01/css-border-image-property/)
\
Neon text (unused, but mentioned in rationale): [CSS Tricks](https://css-tricks.com/how-to-create-neon-text-with-css/)
\
Scroller: [Kevin Powell](https://www.youtube.com/watch?v=iLmBy-HKIAw)
