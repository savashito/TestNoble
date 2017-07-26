'use strict';

const app = require('express')();
const http = require('http').Server(app);
const io = require('socket.io')(http);

class SocketController {
	constructor() {
		this.socket = undefined;
	}

	setSocket(socket) {
		this.socket = socket;
	}
	emit(event,data){
		if(this.socket)
			this.socket.emit(event,data);
		else
			console.log("Socket has not been initialized")
	}
}

let socketController = new SocketController();

io.on('connection', function(socket){
	console.log('a user connected');
	socketController.setSocket(socket);
	/*
	socket.on('ergData', function(data){
		// packageErgEntry(data)
		console.log('message: ' , data);
		socketController.emit('ergData',data);
	});
	socket.on('strokeData', function(data){
		// packageErgEntry(data)
		console.log('strokeData: ' , data);
		socketController.emit('strokeData',data);
	});*/
});

/*
Horno 3 museo
Paseo santalucia.
rentar lancha, macro plaza,
*/


http.listen(8080, function(){
	console.log('listening on *:8080');
});
module.exports = socketController;
// export default  socketController;


