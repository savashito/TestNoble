
class PM5Util {
	public static void SuscribeC2characteristic(characteristic){
		SubscribeCharacteristic (string name, st
ring service, string characteristic, Action<string> notific
ationAction, Action< string, byte[]> action)
	characteristic.subscribe(function(error){
		console.log("Suscribe successfully to characteristic ",characteristic.uuid);
	});
	characteristic.on('data', function(data, isNotification) {
		// console.log(data);
		data = callback(data,ergData);
		if(isErgDataBuilt(ergData)){
			socketController.emit(msgKey,ergData);
			ergData = ErgData();
		}
		if(msgKey=='strokeData'){
			socketController.emit(msgKey,data);
		}
		// socketController.emit(msgKey,data);

		// console.log(callback(data));
	});
}
}