Fnu Alisha SE 572 - Final Project

README

Contains 2 sub-folders:

FilmLibraryApp: contains the App Project. You would need to install the Visual Studio IDE to open and run the project.

WebAndFilmsProject: Contains both the web and film project using docker compose. In order to run both the project, cd to the folder 'webAndFilmProject'. Then run the command- docker-compose build, docker-compose up . It should start the project running on http://192.168.0.7:8080/ The main files changed: style.css, index.html, films.js, app.js, default.conf

Note to run locally: As mentioned in the project requirement, the complete project uses my computer’s local network IP: 192.168.0.7 You'll need to change it to something else by editing the var in films.js{https://github.com/alishaahuja1996/SE572-FilmLibrary/blob/d5a7d4a1b4ac7e093300da8e0610ee2ac582ba96/WebAndFilmsProject/webAndFilmsProject/web/public/javascripts/films.js#L5} file in 'webAndFilmProject' and attribute in RestService.cs
{https://github.com/alishaahuja1996/SE572-FilmLibrary/blob/d5a7d4a1b4ac7e093300da8e0610ee2ac582ba96/App/FilmLibrary/FilmLibrary/AppDataManager/RestService.cs#L22} in 'FilmLibrary' projects. Once done, the project should run on your set IP address url format http://xxx.xxx.xxx.xxx:8080/

Kaltura Link: https://1513041.mediaspace.kaltura.com/media/SE572-FinalProject/0_n0v37ga6
