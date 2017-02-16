
var noble = require('noble');

var manageBufferErg = require('./manageBufferErg');
var socket = require('./socketController');
var uuidErgService = '6969';
// var uuidErgService = '18902a9a-1f4a-44fe-936f-14c8eea41800';// '6969';
var uuidErgCharacteristic = '6970';

console.log(socket);
/*
function2 = function () {
	socket.emit("ergData",{msg:"hey"})
	setTimeout(function2, 200);
}

setTimeout(function2, 1);
// */
// process.exit()
/*
console.log("Sexy");
noble.on('stateChange', function(state) {
	if (state === 'poweredOn') {
		console.log('scanning for '+uuidErgService+' ...');
		noble.startScanning([uuidErgService], false);
	}
	else {
		noble.stopScanning();
	}
});

var ergService = null;
var ergCharacteristic = null;

noble.on('discover', function(peripheral) {
	noble.stopScanning();
	console.log('found peripheral:', peripheral.advertisement);
	peripheral.connect(function(err) {
		peripheral.discoverServices([uuidErgService], function(err, services) {
			services.forEach(function(service) {
				ergService = ergService;
				console.log('found service:', service.uuid);
				service.discoverCharacteristics([], function(err, characteristics) {
					characteristics.forEach(function(characteristic) {
						console.log('found characteristic:', characteristic.uuid);
						if (uuidErgCharacteristic == characteristic.uuid) {
							ergCharacteristic = characteristic;
							requestErgData();
						}
					});
				})
			})
		})
	})
})

function requestErgData() {
	var requestTelemetry = [1,2,3,4];
	var ergData = Buffer.from(requestTelemetry);
	ergCharacteristic.write(ergData, false, function(err) {
		if (!err) {
			console.log('Requesting write');

			ergCharacteristic.read(undefined);
			ergCharacteristic.on('read', function(data, isNotification) {
				var ergData = manageBufferErg.unPackageErgEntry(data);
				console.log('Erg response 2',data,ergData);
				socket.emit('ergData',ergData);
				// socket.emit('ergDataBynary',{data:data});
				ergCharacteristic.read(undefined);
			});
					
		}
		else {
			console.log('toppings error',err);
		}
	});
}
*/