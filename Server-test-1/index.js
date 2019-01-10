// API HTML

//var restify = require('restify');
//var express = require('express');
//var bodyParser = require('body-parser');
//var counter = 0;
var app = express();
app.use(bodyParser.urlencoded({
    extended: true
  }));
app.use(bodyParser.json());

app.get('/cars', function(req, res, next) {
    console.log("LVOV GET");
    res.send('List of cars: [TODO]');
    return next();
});

app.get('/cars/:plate', function(req, res, next) {
    console.log("LVOV GET SPEC");
    res.send('Current values for car ' + req.params['plate'] + ': [TODO]');
    return next();
});

app.post('/cars/:plate', function(req, res, next) {

    // uncomment to see posted data
    //console.log("Next: " + res.body);
    console.log('LVOV ' + JSON.stringify(req.body) + " " + counter++);
    //console.log(req.body);

    res.status(200);
    res.write('Data received from plate [TODO]');
    res.end();

    return next();
});

var server = http.createServer(app);
server.name = "Italo-PC";
//server.url = "192.168.101.48";

server.listen(8080, function() {
    console.log('%s listening at %s', server.name, server.url);
});