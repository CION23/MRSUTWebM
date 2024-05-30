




///////////////////////////////////////////////////////////////////////////////////////////////
$(document).ready(function () {
     $(document).on('click', function (event) {
          var clickover = $(event.target);
          var opened = $('.navbar-collapse').hasClass('show');
          var navToggler = $('.navbar-toggler');
          if (opened === true && !clickover.hasClass('navbar-toggler') && clickover.parents('.navbar-collapse').length === 0) {
               navToggler.click();
          }
     });
});

///////////////////////////////////////////////////////// Delete User Confirmation Control for Admin/////////////////////////////////////////////////////////////////////////
function confirmDelete(userId) {
     document.querySelector('#deleteUserForm input[name="userId"]').value = userId;
     $('#deleteUserModal').modal('show');
}

///////////////////////////////////////////////////////// Modify User Control for Admin/////////////////////////////////////////////////////////////////////////
function openModifyModal(userId, firstName, lastName, userName, emailAddress, password, ip) {
     document.querySelector('#modifyUserForm input[name="userId"]').value = userId;
     document.querySelector('#modifyUserForm input[name="firstName"]').value = firstName;
     document.querySelector('#modifyUserForm input[name="lastName"]').value = lastName;
     document.querySelector('#modifyUserForm input[name="userName"]').value = userName;
     document.querySelector('#modifyUserForm input[name="emailAddress"]').value = emailAddress;
     document.querySelector('#modifyUserForm input[name="password"]').value = password;
     document.querySelector('#modifyUserForm input[name="ip"]').value = ip;
     $('#modifyUserModal').modal('show');
}

///////////////////////////////////////////////////////// Adding Music Modal /////////////////////////////////////////////////////////////////////////
$(document).ready(function () {
     // Function to filter genres based on search input
     $('#genreSearchInput').on('input', function () {
          var searchText = $(this).val().toLowerCase();
          $('.genre-badge').each(function () {
               var genreName = $(this).text().toLowerCase();
               var isVisible = genreName.includes(searchText);
               $(this).toggle(isVisible);
          });
     });

     // Function to handle genre selection
     $(document).on('click', '.genre-badge', function () {
          var genreId = $(this).data('genre-id');
          var genreName = $(this).text();
          $('#selectedGenreId').val(genreId); // Set the selected genre ID to the hidden input field
          $('#genreSearchInput').val(genreName); // Set the input field value to the selected genre name
          $('#genreList').hide(); // Hide the genre list after selection
     });

     // Show genre list when the input field gains focus
     $('#genreSearchInput').focus(function () {
          $('#genreList').show();
     });

     // Hide genre list when clicking outside
     $(document).click(function (event) {
          if (!$(event.target).closest('#genreSearchInput, #genreList').length) {
               $('#genreList').hide();
          }
     });

     // Validate selected genre before form submission
     $('#addMusicForm').submit(function (e) {
          var selectedGenreId = $('#selectedGenreId').val();
          if (!selectedGenreId) {
               e.preventDefault(); // Prevent form submission if no genre is selected
               alert('Please select a valid genre.');
          } else {
               // Continue with form submission
               setFileNames(); // Ensure file names are set correctly
          }
     });
});

// Image and Music Name
function setFileNames() {
     var musicFile = document.getElementById("musicFile");
     var imageFile = document.getElementById("imageFile");
     var musicNameInput = document.getElementById("musicName");
     var imageNameInput = document.getElementById("imageName");

     // Get the file names
     var musicFileName = musicFile.files.length > 0 ? musicFile.files[0].name : "";
     var imageFileName = imageFile.files.length > 0 ? imageFile.files[0].name : "";

     // Set the file names in the hidden inputs
     musicNameInput.value = musicFileName;
     imageNameInput.value = imageFileName;
}

///////////////////////////////////////////////////////// Music Button Home /////////////////////////////////////////////////////////////////////////
$(document).ready(function () {
     var currentAudio = null; // Variable to store the currently playing audio
     var currentIndex = 0; // Index of the current music in the list

     $('.play-button').click(function () {
          var audio = $(this).siblings('.audio-player')[0];
          if (audio !== currentAudio) {
               // Pause the currently playing audio if it's different from the clicked one
               if (currentAudio !== null) {
                    currentAudio.pause();
                    $(currentAudio).siblings('.play-button').find('i').removeClass('fa-pause').addClass('fa-play'); // Change icon to play for the previous button
               }
               currentAudio = audio;
          }

          if (audio.paused) {
               audio.currentTime = 0; // Start playing from the beginning
               audio.play();
               $(this).find('i').removeClass('fa-play').addClass('fa-pause'); // Change icon to pause
          } else {
               audio.pause();
               $(this).find('i').removeClass('fa-pause').addClass('fa-play'); // Change icon to play
          }
     });

     // Function to change icon to play
     function changeIconToPlay(index) {
          $('.play-button').eq(index).find('i').removeClass('fa-pause').addClass('fa-play'); // Change icon to play for the specified index
     }

     // Automatically play the next audio when the current one ends
     $('.audio-player').on('ended', function () {
          changeIconToPlay(currentIndex); // Change icon to play for the current button
          currentIndex++; // Move to the next music in the list
          var nextAudio = $('.audio-player').eq(currentIndex)[0];
          if (nextAudio) {
               nextAudio.currentTime = 0;
               nextAudio.play();
               currentAudio = nextAudio;
               $('.play-button').eq(currentIndex).find('i').removeClass('fa-play').addClass('fa-pause'); // Change icon to pause for the next button
          }
     });
});


////////////////////////////////////////////////////////// Search button view //////////////////////////////////////////////////////////////
$(document).ready(function () {
     $('#searchForm').submit(function (e) {
          e.preventDefault();
          var searchText = $('#searchText').val().trim();
          if (searchText.length > 0) {
               $.ajax({
                    url: '@Url.Action("Search", "Music")',
                    type: 'GET',
                    data: { searchText: searchText },
                    success: function (data) {
                         $('#searchResults').html(data);
                    },
                    error: function () {
                         alert('Error fetching search results.');
                    }
               });
          }
     });
});
////////////////////////////////////////////////////////// Genres View //////////////////////////////////////////////////////////////
$(document).ready(function () {
     $('.genre-link').click(function (e) {
          e.preventDefault();
          var genreId = $(this).data('genreid');
          if (genreId !== null && genreId !== undefined) {
               window.location.href = '@Url.Action("Genres", "Home")?genreId=' + genreId;
          } else {
               console.error('GenreId is null or undefined.');
          }
     });
});

//////////////////////////////////////////////////////////Most wathced button //////////////////////////////////////////////////////////////
document.addEventListener('DOMContentLoaded', function () {
     const buttons = document.querySelectorAll('.toggle-button');
     buttons.forEach(button => {
          button.addEventListener('click', () => {
               console.log('Button clicked'); // Add this line for debugging
               const target = button.getAttribute('data-target');
               const section = document.getElementById(target);
               if (section.style.display === 'none') {
                    section.style.display = 'block';
               } else {
                    section.style.display = 'none';
               }
          });
     });
});

//////////////////////////////////////////////////////////Increment for Most Watched//////////////////////////////////////////////////////////////
var isPlaying = false; // Flag to track play state
var musicId = null; // Variable to store the currently playing music ID

function playMusic(clickedMusicId) {
     if (!isPlaying || musicId !== clickedMusicId) {
          // Increment listen count only if not already playing or if the clicked music is different
          $.ajax({
               type: "POST",
               url: "/Home/IncrementListenCount",
               data: { musicId: clickedMusicId },
               success: function () {
                    // Incremented listen count successfully
                    musicId = clickedMusicId; // Update currently playing music ID
                    isPlaying = true; // Set play state to true
               },
               error: function () {
                    // Error incrementing listen count
               }
          });
     } else {
          isPlaying = false; // Set play state to false on pause
     }
}