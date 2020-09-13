const express = require("express");
const app = express();
const bodyParser = require("body-parser");
const cors = require("cors");
const Film = require("./models/film_model");
const jwt = require("jsonwebtoken");

app.use(bodyParser.json());
app.use(cors());

app.get("/", (req, res) => {
  res.json({ msg: "films" });
});

app.get("/api/v1/films", async (req, res) => {
  const films = await Film.find({});
  res.json(films);
});

app.post("/api/v1/films", verifyToken, async (req, res) => {
  if((req.body.name == null || req.body.name == "") && (req.body.rating == null || req.body.rating == "")){
    res.sendStatus(400);
    console.log("Save Film: Film name and/or rating is null/empty");
  }
  else {
    const film = new Film({ name: req.body.name, rating: req.body.rating });
    const savedFilm = await film.save();
    res.json(savedFilm);
  }
});

app.put("/api/v1/modifyRating", async (req, res) => {
  if((req.body.name == null || req.body.name == "") && (req.body.rating == null || req.body.rating == "")){
    res.sendStatus(400);
    console.log("Modify Rating: Film name and/or rating is null/empty");
  }
  else {
    const modifiedFilm = await Film.updateOne(
      { name: req.body.name },
      {
        $set: { rating: req.body.rating }
      }, 
      function(err, res) {
      if (err) throw err;
      }
    );
    res.json(modifiedFilm);
  }
});

app.post("/api/v1/login", (req, res) => {
  if(req.body.username == null || req.body.username == ""){
    res.sendStatus(400);
    console.log("Login: Username is null/empty");
  }
  else {
    const user = { 
      name: req.body.username
    };
    jwt.sign({user}, 'secretKey', (err, token) => {
      res.json({
        token
      })
    });
  }
});

function verifyToken (req, res, next) {
  const bearerHeader = req.headers['authorization'];
  
  if (typeof bearerHeader !== 'undefined') {
    const bearerToken = bearerHeader.split(' ')[1];
    
    jwt.verify (bearerToken, 'secretKey', (err, authData) => {
      if (err) {
        res.sendStatus(403);
      }
      else {
        next();
      }
    })
  }
  else {
    res.sendStatus(403);
  }
}

module.exports = app;