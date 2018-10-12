var express = require('express');
var app = express();
var path = require("path");
var engines = require('consolidate');

var jeu = require('./routes/jeu');

app.use(express.static(__dirname + '/public'));

app.set('views', path.join(__dirname, 'views'));
app.engine('html', engines.mustache);
app.set('view engine', 'html');

/*
app.get('/', function (req, res, ) {
    res.sendFile(path.join(__dirname + '/views/index.html'));
})
*/

app.use('/', jeu);

app.listen(process.env.PORT || 3000, function () {
    console.log('Listening on http://localhost:' + (process.env.PORT || 3000))
})

module.exports = app;