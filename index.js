
var noble = require('noble');
var manageBufferPM5C2Erg = require('./manageBufferPM5C2Erg');
var socket = require('./socketController');
var PM5Util = require('./concept2PM5Util');


console.log(socket);
PM5Util.setSocketController(socket);
console.log("REC list");
noble.on('stateChange', function(state) {
	if (state === 'poweredOn') {
		console.log('scanning for Concept 2 ergs ...');
		noble.startScanning(true);
	}
	else {
		console.log("Please turn on bluethoot");
		noble.stopScanning();
	}
});



noble.on('discover', function(peripheral) {

	// noble.stopScanning();
	console.log('found peripheral:', peripheral.advertisement);
	if(peripheral.advertisement && peripheral.advertisement.localName){
		const name = peripheral.advertisement.localName;
		console.log("Found ",name);
		if(name.includes("PM5")){
			console.log("Found ",name);
			peripheral.connect(function(err) {
				// get C2 rowing primary service 
				PM5Util.getC2PrimaryService(peripheral).then(function(service){
					return PM5Util.getC2GeneralStatusCharacteristic(service);
				})
				.then(function(characteristics) {
					PM5Util.suscribeC2characteristic(characteristics[0],'ergData',manageBufferPM5C2Erg.unPackageErgEntry31);
					PM5Util.suscribeC2characteristic(characteristics[1],'ergData',manageBufferPM5C2Erg.unPackageErgEntry32);
					PM5Util.suscribeC2characteristic(characteristics[2],'ergData',manageBufferPM5C2Erg.unPackageErgEntry33);
					PM5Util.suscribeC2characteristic(characteristics[5],'ergData',manageBufferPM5C2Erg.unPackageErgEntry3A);
					// called every stroke
					PM5Util.suscribeC2characteristic(characteristics[3],'strokeData',manageBufferPM5C2Erg.unPackageErgEntry35);
					PM5Util.suscribeC2characteristic(characteristics[4],'ergData',manageBufferPM5C2Erg.unPackageErgEntry36);

					
				});
			});
		}	
	}
});

















////// OLD TUCSON
/*
var manageBufferErg = require('./manageBufferErg');
var uuidErgService = '6969';
// var uuidErgService = '18902a9a-1f4a-44fe-936f-14c8eea41800';// '6969';
var uuidErgCharacteristic = '6970';

function2 = function () {
	socket.emit("ergData",{msg:"hey"})
	setTimeout(function2, 200);
}

setTimeout(function2, 1);
// 
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