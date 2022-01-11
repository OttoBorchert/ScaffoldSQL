// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function activeLinkControl() {
    let id = null;
    // If index
    if (window.location.pathname === "/") {
        id = "Home";
    } else {
        // If not index, strip out the / and space
        id = window.location.pathname.replace(/(\/| )/g, '');
    }
    // Get the element and add the class
    let element = document.getElementById('nav' + id);
    if (element) {
        document.getElementById("nav" + id).classList.add('active');
    } else {
        // The element doen't exist. Instead of logging an excpetion, log the fact that is failed. This could be intentional.
        console.log(`Could not find the page identifier ${id} in the navigation bar. Is this a page for internal testing? If you are a student or other
end user, please report this issue.`);
    }

    // Hide the navbar
    if (window !== window.top) {
        document.getElementById('primary_navbar').classList.add('hide');
    }
}

// Run the activeLink Control on page load
$(document).ready(activeLinkControl);

function SanitizeHTMLParsons(string) {
    const toReplace = {
        '<': '&lt;',
        '>': '&gt;',
        '&': '&amp;',
        ':': '&colon;',
        '\'': '&apos;',
        '"': '&quot;'
    }
    return string.replace(new RegExp(Object.keys(toReplace).join('|'), 'g'), str => toReplace[str]);
}

function DesanitizeHTMLParsons(string) {
    const toReplace = {
        '&lt;': '<',
        '&gt;': '>',
        '&amp;': '&',
        '&colon;': ':',
        '&apos;': '\'',
        '&quot;': '"'
    }
    return string.replace(new RegExp(Object.keys(toReplace).join('|'), 'g'), str => toReplace[str]);
}