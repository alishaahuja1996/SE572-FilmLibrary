var API = (() => {
    // variable to store the JWT token locally
    var jwtToken;
    // variable to store the URL
    var url = "http://192.168.0.7:8080";

    /**
     * login:
     * Responsible for logging in the user to
     * authorize film additions to the films library.
     */
    var login = () => {
        // Get input from username textbox
        const val = document.getElementById("login").value;

        // Validation Check for invalid and empy username fields
        // Provide error message to the user, if error found
        if(val == null || val == ''){
            handingMessage("errorLoginMsg", "Must enter username");
            return false;
        } else if(isInvalidValue(val)){
            handingMessage("errorLoginMsg", "Must enter a valid username");
            return false;
        }

        // Make a POST request to server to get and store JWT token
        try {
            fetch(url + "/api/v1/login", {
                method: 'POST',
                body: JSON.stringify({
                    username: val,
                }),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                }
            }).then (resp => resp.json())
                .then (data => {
                    jwtToken = data.token;
                });
        } catch (e){
            console.log(e);
            console.log("-----------------");
        }

        // reset the form for new submission
        var inputLoginForm = document.getElementsByName("inputLoginForm")[0];
        inputLoginForm.reset();
        // Provide a successful submission message to user
        handingMessage("successLoginMsg", "User authorized successfully!");

        return false;
    }
    
    /**
     * createFilm:
     * Responsible for adding user input 'film title'
     * and 'film ratyting'
     * to the films library.
     */
    var createFilm = () => {
        // Get input from film title and film rating textbox
        var filmTitle = document.getElementById("filmTextbox").value;
        var filmRating = document.getElementById("ratingTextbox").value;
        
        // Validation Check for invalid and empy fields
        // Provide error message to the user, if error found
        if((filmTitle == null || filmTitle == '') && (filmRating == null || filmRating == '')){
            handingMessage("errorMsg", "Must enter both film title and film rating");
            return false;
        } else if(filmTitle == null || filmTitle == ''){
            handingMessage("errorMsg", "Must enter a film title");
            return false;
        } else if(isInvalidValue(filmTitle)){
            handingMessage("errorMsg", "Must enter a valid film title");
            return false;
        } else if(filmRating == null || filmRating == ''){
            handingMessage("errorMsg", "Must enter a film rating");
            return false;
        } else if(isNaN(filmRating) || filmRating < 1 || filmRating > 5){
            handingMessage("errorMsg", "Film rating should be a number ranging from 1 to 5");
            return false;
        }

        // sanitize the film title for injection
        var val = encodeHTML(filmTitle);

        // Make a POST request to server to add the film title and rating to library
        try {
            fetch(url + "/api/v1/films", {
                method: 'POST',
                body: JSON.stringify({
                    name: val,
                    rating: filmRating
                }),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + jwtToken
                }
            }).then(res => {
                setTimeout(function () {
                    if(res.status == 200) {
                        // Provide a successful submission message to user
                        handingMessage("successMsg", "Successfully added film in the library!");
                    }
                    else {
                        // Provide a error message and error code to user
                        handingMessage("errorMsg", "Error " + res.status + ": " + res.statusText + ". User is not authorized to add films to the library!");
                    }
                }, 0);
            });
        } catch (e){
            console.log(e);
            console.log("-----------------");
        }

        // reset the form for new submission
        var inputForm = document.getElementsByName("inputForm")[0];
        inputForm.reset();

        return false;
    }

    /**
     * getFilms:
     * Responsible for showing the current film title and
     * film ratings, and ability to modify film rating
     * as a table.
     */
    var getFilms = () => {
        // Make a GET request to server to get all films in library
        try {
            fetch(url + "/api/v1/films", {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            }).then(resp => resp.json())
                .then(results => {
                    if(results.length == 0){
                        // Provide an informed message for empty film library
                        handingMessage("successViewFilmsMsg", "No films exist in the library!");

                        // Hide the table
                        var table = document.getElementById("filmTable");
                        table.classList.remove("show");
                        table.classList.add("hidden");
                    }
                    else{
                        // Create table for film library
                        var body = document.getElementById("tbody");
                        body.innerHTML = " ";

                        results.forEach(data => {
                            var row = document.createElement("tr");
                            var tdFilmTitle = document.createElement("td");
                            var tdRating = document.createElement("td");
                            var modifyRatingTextbox = document.createElement("input");
                            var modifyBtn = document.createElement("input");

                            tdFilmTitle.innerHTML = data.name;
                            tdRating.innerHTML = data.rating + "/5";

                            modifyRatingTextbox.setAttribute('name', data.name);
                            modifyRatingTextbox.setAttribute("type", "text");
                            modifyRatingTextbox.classList.add("newRatingTextbox");

                            modifyBtn.setAttribute('id', data.name);
                            modifyBtn.setAttribute("type", "Submit");
                            modifyBtn.setAttribute("value", "Modify Rating");
                            modifyBtn.classList.add("newRatingButton");

                            //for each button, call the 'function(event)' when clicked
                            modifyBtn.addEventListener('click', function (event) {  
                                // 'this' refers to the current button on the for loop
                                var existingFilmName = this.id;
                                modifyFilmRating(existingFilmName);
                            });

                            row.appendChild(tdFilmTitle);
                            row.appendChild(tdRating);
                            row.appendChild(modifyRatingTextbox);
                            row.appendChild(document.createTextNode("\u00A0\u00A0\u00A0"));
                            row.appendChild(modifyBtn);

                            body.append(document.createElement("br"));
                            body.append(row);
                        });

                        // Show and Prettify the table
                        var table = document.getElementById("filmTable");
                        table.classList.remove("hidden");
                        table.classList.add("show", "table");
                    }
                });
        } catch (e){
            console.log(e);
            console.log("-----------------");
        }

        return false;
    }

    /**
     * modifyFilmRating:
     * Responsible for modifying ratings
     * for existing films in the library.
     * @param {string} existingFilmName
     */
    var modifyFilmRating = (existingFilmName) => {
        var newFilmRating = document.getElementsByName(existingFilmName)[0].value;
    
        // Validation Check for invalid and empy fields
        // Provide error message to the user, if error found
        if(newFilmRating == null || newFilmRating == ''){
            handingMessage("errorViewFilmsMsg", "Must enter a new film rating");
        } else if(isNaN(newFilmRating) || newFilmRating < 1 || newFilmRating > 5){
            handingMessage("errorViewFilmsMsg", "New film rating should be a number ranging from 1 to 5");
        } else {
            // Make a PUT request to server to modify existing film rating in library
            try {
                fetch(url + "/api/v1/modifyRating", {
                    method: 'PUT',
                    body: JSON.stringify({
                        name: existingFilmName,
                        rating: newFilmRating
                    }),
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    }
                }).then(res => {
                    setTimeout(function () {
                        if(res.status == 200) {
                            // Provide a successful submission message to user
                            handingMessage("successViewFilmsMsg", "Successfully modified film rating in the library!");
                        }
                        else {
                            // Provide a error message and error code to user
                            handingMessage("errorViewFilmsMsg", "Error " + res.status + ": " + res.statusText + ".");
                        }
                    }, 0);
                });
            } catch (e){
                console.log(e);
                console.log("-----------------");
            }
        }
    }

    /**
     * handlingMessage:
     * Helper function responsible for showcasing of 
     * messages to the user for time 2 second.
     * @param {string} msgStringID 
     * @param {string} msgString 
     */
    var handingMessage = (msgStringID, msgString) => {
        // Show message to user
        var msg = document.getElementById(msgStringID);
            msg.innerHTML = msgString;
            msg.classList.remove("hidden");
            msg.classList.add("show");
        
        // Hide message from user after 2 second
        setTimeout(function(){
            msg.classList.remove("show");
            msg.classList.add("hidden");
        }, 2000);
    }

    /**
     * encodeHTML:
     * Responsible to prevent HTML and JS injections
     * on user input.
     * @param {string} inputString 
     */
    var encodeHTML = (inputString) => {
        return inputString.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/"/g, '&quot;');
    }

    /**
     * isInvalidValue:
     * Responsible to check for invalid input value characters
     * on user input. (used for filmTitle and username)
     * @param {string} inputValue
     */
    var isInvalidValue = (inputValue) => {
        // Assumes that any type of characters of length 2 or above are valid 
        var isInvalid= false;
        var invalidChars = [",", ".", "/", "\\", ";", ":", "<", ">", "?", "[","]", "@", "!", "#", "$", "%", "^", "*"];

        // Check invalid case: if only spaces are entered
        if (!inputValue.replace(/\s/g, '').length){
            isInvalid = true;
        } 
        // Check invalid case: if only single invalid character is entered
        else if(inputValue.length == 1){
            for(let invalidChar of invalidChars){
                if(invalidChar == inputValue){
                    isInvalid = true;
                    break;
                }
            }
        } 
    
        return isInvalid;
    }

    return {
        login,
        createFilm,
        getFilms
    }
})();