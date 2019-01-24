// API AMQP
var amqp = require('amqplib/callback_api');
var amqp2 = require('amqplib/callback_api');
var hostname = 'amqp://vdznsexb:3gE_psy4Q32UB1hgzyOy1-ovSt7vHmUr@golden-kangaroo.rmq.cloudamqp.com/vdznsexb';

// Queue 1: riceve messaggi in fanout
amqp.connect(hostname, function(err, conn) {
    conn.createChannel(function(err, channel) {
        var ex = 'logs';

        channel.assertExchange(ex, 'fanout' , {durable: false});
        channel.assertQueue('', {exclusive: true}, function(err, q) {
            console.log(" [*] Waiting for messages in %s. To exit press CTRL+C", q.queue);
            channel.bindQueue(q.queue, ex, '');

        // Aspetta i messaggi
            setTimeout(() => {
                channel.consume(q.queue, function(msg) {
                    if(msg.content) {
                        console.log(" [x] %s", msg.content.toString());
                    }
                }, {noAck: true});
            }, 2000);
        });
    });
});

// Queue 2: riceve messaggi in fanout
amqp2.connect(hostname, function(err, conn) {
    // Si connette e si assicura che esista il server
    conn.createChannel(function(err, channel) {
        var ex = 'logs';

        channel.assertExchange(ex, 'fanout' , {durable: false});
        channel.assertQueue('', {exclusive: true}, function(err, q) {
            console.log(" [*] Waiting for messages in %s. To exit press CTRL+C", q.queue);
            channel.bindQueue(q.queue, ex, '');

        // Aspetta i messaggi
            setTimeout(() => {
                channel.consume(q.queue, function(msg) {
                    if(msg.content) {
                        console.log(" [x] %s - Second receiver", msg.content.toString());
                    }
                }, {noAck: true});
            }, 2000);
        });
    });
});


/*
// API CoAP

var coap        = require('coap');
var server      = coap.createServer();

server.on('request', function(req, res) {
  console.log('Arrivata una richiesta\nMessaggio: ' + req.payload.toString() 
  //+ '\nID: ' + res
  +'\n');
  //console.log(req);
  res.end('Hello ' + req.url.split('/')[1] + '\n');
})

// the default CoAP port is 5683
server.listen(function() {
})
*/


/*
// API MQTT

var mqtt = require('mqtt');

var client = mqtt.connect('mqtt:\\\\test.mosquitto.org');

client.on('connect', function(){
    client.subscribe('kitt/auto/+/+', function (err) {
        if (!err) {
          client.publish('kitt/auto/targa/temperatura', 'BELLA RAGA 130 MARTIN GARRIX SI VOLAAAAAAAA');
        }
    })
})
    
client.on('message', function (topic, message) {
    // message is Buffer
    console.log(message.toString());
    console.log(topic.toString());

    if (topic.includes('/')) {
        
    }
    //client.end()
})
*/

/*
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
 */