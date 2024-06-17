// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Listings/Index - Start
// Ensure jQuery is loaded before this script
$(document).ready(function () {
    // Handle click event on the toggle button
    $('#toggleFormButton').click(function () {
        $('#filterFormContainer').slideToggle(); // Toggle visibility of the form section
    });
});
// Listings/Index - End


// Listings/Details - Start
// Favorite button
document.addEventListener('DOMContentLoaded', function () {
    // Favorite Button Click Event
    const favoriteButton = document.getElementById("favoriteButton");
    if (favoriteButton) {
        favoriteButton.addEventListener("click", function () {
            var button = this; // Get the button element that triggered the click event
            var listingId = button.getAttribute('data-listing-id'); // Get the listing ID from a data attribute

            // Send an AJAX request to the server to favorite the listing
            fetch(`/Listings/Details?id=${listingId}&handler=FavoriteListingHandler`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value // Add anti-forgery token if needed
                },
                body: JSON.stringify({})
            })
                .then(response => {
                    if (response.ok) {
                        // Listing favorited successfully
                        console.log("Listing favorited successfully.");
                        // Toggle button class
                        button.classList.toggle("btn-secondary");
                        button.classList.toggle("btn-primary");
                        // Toggle button text
                        button.innerText = button.innerText === 'Favorite' ? 'Unfavorite' : 'Favorite';
                    } else {
                        // Failed to favorite listing
                        console.error("Failed to favorite listing.");
                    }
                })
                .catch(error => {
                    console.error("Error:", error);
                });
        });
    }

    // Star Rating Handling
    let ratingValue = 0; // Initialize rating value

    const ratingContainer = document.querySelector('.rating');
    if (ratingContainer) {
        ratingContainer.addEventListener('mouseleave', () => {
            // Reset star images to their default state when mouse leaves the rating container
            document.querySelectorAll('.star').forEach(s => {
                const sValue = parseInt(s.getAttribute('data-value'));
                if (sValue <= ratingValue) {
                    s.querySelector('.star-image').classList.add('star-image-filled');
                } else {
                    s.querySelector('.star-image').classList.remove('star-image-filled');
                }
            });
        });

        document.querySelectorAll('.star').forEach(star => {
            star.addEventListener('mouseenter', () => {
                const hoveredValue = parseInt(star.getAttribute('data-value'));

                // Change the images of stars based on the hover event
                document.querySelectorAll('.star').forEach(s => {
                    const sValue = parseInt(s.getAttribute('data-value'));
                    if (sValue <= hoveredValue) {
                        s.querySelector('.star-image').classList.add('star-image-filled');
                    } else {
                        s.querySelector('.star-image').classList.remove('star-image-filled');
                    }
                });
            });

            star.addEventListener('click', () => {
                ratingValue = parseInt(star.getAttribute('data-value'));

                // Fill all stars up to the clicked one
                document.querySelectorAll('.star').forEach(s => {
                    const sValue = parseInt(s.getAttribute('data-value'));
                    if (sValue <= ratingValue) {
                        s.querySelector('.star-image').classList.add('star-image-filled');
                    } else {
                        s.querySelector('.star-image').classList.remove('star-image-filled');
                    }
                });

                // Send the ratingValue to the server using AJAX
                const listingId = ratingContainer.getAttribute('data-listing-id'); // Get the listing ID from the rating container
                console.log('Rating value:', ratingValue);
                fetch(`/Listings/Details?id=${listingId}&rating=${ratingValue}&handler=RateListing`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value // Add anti-forgery token if needed
                    }
                })
                    .then(response => {
                        if (response.ok) {
                            // Rating submitted successfully
                            console.log("Rating submitted successfully.");
                            // Update UI to reflect the selected rating if needed
                        } else {
                            // Failed to submit rating
                            console.error("Failed to submit rating.");
                        }
                    })
                    .catch(error => {
                        console.error("Error:", error);
                    });
            });
        });
    }
});
// Listings/Details - End

// ImageUploadValidation - Start
function validateImageFiles() {
    const input = document.getElementById('ImageFiles');
    const files = input.files;
    const allowedExtensions = ['.jpg', '.png'];
    let isValid = true;

    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        const fileExtension = '.' + file.name.split('.').pop().toLowerCase();
        if (!allowedExtensions.includes(fileExtension)) {
            isValid = false;
            break;
        }
    }

    if (!isValid) {
        input.value = '';
        alert('Please select only .jpg or .png files.');
    }
}
// ImageUploadValidation - End
