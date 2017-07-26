using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLEConcept2PM5 : MonoBehaviour {


	private byte[] data;
	private BluetoothDeviceScript bluetoothDeviceScript;
	// private Action<string,byte[]> actionReadErgData;

	private IErgDataCallbacks ergCallbacks;
	// Use this for initialization
	public void Init (IErgDataCallbacks ergCallbacks) {
		this.ergCallbacks = ergCallbacks;
		// output.text = "Initializing bluetooth";
		// byte[] data = new byte[] {69,9};
		// output1.text = String.Format("{0}{1}",data[0],data[1]);
		bluetoothDeviceScript = BluetoothLEHardwareInterface.Initialize (true, false, () =>{
			// output.text = "Diente azul inicializado";
			ScanForPM5Ergs();
		}, null);
		// initActions ();
	}

	void toLongUUID(string shortUUID){
		return "ce06"+shortUUID+"43e511e4916c0800200c9a66";
	}

	bool isShortCharacteristic(string lUUID, string sUUID){
		return characteristicUUID == toLongUUID(sUUID);
	}

	void ScanForPM5Ergs(){
		StringComparison comp = StringComparison.OrdinalIgnoreCase;
		BluetoothLEHardwareInterface.ScanForPeripheralsWithServices (new string[] {toLongUUID("0030")}, 
		(address, name) => {
  			if(name.Contains("PM5", comp)){
				output.text = "Found "+name;
				output1.text = "Addr: "+address;
				BluetoothLEHardwareInterface.ConnectToPeripheral (address, (addressPeriferal) => {},
				(addressPeriferal, serviceUUID) => {},
				(addressPeriferal, serviceUUID, characteristicUUID) => {
					// This should be C2 PM5 primary service 
					Action<string,byte[]> actionSuscribeCharacteristic = manageBufferPM5C2Erg.unPackageErgEntryGeneric;
					if(isShortCharacteristic(characteristicUUID,"0031"))
						actionSuscribeCharacteristic = manageBufferPM5C2Erg.unPackageErgEntry31;
					else if(isShortCharacteristic(characteristicUUID,"0032"))
						actionSuscribeCharacteristic = manageBufferPM5C2Erg.unPackageErgEntry32;
					else if(isShortCharacteristic(characteristicUUID,"0033"))
						actionSuscribeCharacteristic = manageBufferPM5C2Erg.unPackageErgEntry33;
					else if(isShortCharacteristic(characteristicUUID,"0035"))
						actionSuscribeCharacteristic = manageBufferPM5C2Erg.unPackageErgEntry35;
					else if(isShortCharacteristic(characteristicUUID,"0036"))
						actionSuscribeCharacteristic = manageBufferPM5C2Erg.unPackageErgEntry36;
					else if(isShortCharacteristic(characteristicUUID,"003A"))
						actionSuscribeCharacteristic = manageBufferPM5C2Erg.unPackageErgEntry3A;
					BluetoothLEHardwareInterface.SubscribeCharacteristic (address, serviceUUID, characteristicUUID, actionSuscribeCharacteristic);
	

					// PM5Util.SuscribeC2characteristic(characteristicUUID);
					
					// PM5Util.SuscribeC2characteristic(characteristics[1],"ergData",manageBufferPM5C2Erg.unPackageErgEntry32);
					// PM5Util.SuscribeC2characteristic(characteristics[2],"ergData",manageBufferPM5C2Erg.unPackageErgEntry33);
					// PM5Util.SuscribeC2characteristic(characteristics[5],"ergData",manageBufferPM5C2Erg.unPackageErgEntry3A);
					// // called every stroke
					// PM5Util.SuscribeC2characteristic(characteristics[3],"strokeData",manageBufferPM5C2Erg.unPackageErgEntry35);
					// PM5Util.SuscribeC2characteristic(characteristics[4],"ergData",manageBufferPM5C2Erg.unPackageErgEntry36);

					/*
					// _connectedID = addressPeriferal;
					_serviceUUID = serviceUUID;
					_characteristicUUID = characteristicUUID;
					output1.text = String.Format("{0}: {1} {2} {3}",characteristicUUID,ergServiceUUID,addressPeriferal,characteristicUUID.ToString());
					output.text = characteristicUUID;
					// We are connected, now request a read
					BluetoothLEHardwareInterface.ReadCharacteristic (_connectedID,_serviceUUID,_characteristicUUID, 
						//							actionReadErgData
						(string nameCharacteristic, byte[] data) => {
							//							File.WriteAllBytes("Foo.txt", data); // Requires System.IO

							output.text = String.Format("{0}|{1}|{2}|{3}",data[0],data[1],data[2],data[3]);
							ErgData erg = ErgData.FromBytes(data);
							//								OnErgData(erg);
							factoryCommunication.OnErgData(erg);
							output1.text = String.Format("{0}",erg.ToString());
							//			output1.text = String.Format("We got this {0} {1}",BitConverter.IsLittleEndian,data.Length);
							BluetoothLEHardwareInterface.ReadCharacteristic (_connectedID,_serviceUUID,_characteristicUUID, actionReadErgData);
						}



					);
					//
					*/

				});
			}
		});
	}

	void initActions(){
		actionReadErgData = (string nameCharacteristic, byte[] data) => {
			//							File.WriteAllBytes("Foo.txt", data); // Requires System.IO

			output.text = String.Format("{0}|{1}|{2}|{3}",data[0],data[1],data[2],data[3]);
			ErgData erg = ErgData.FromBytes(data);
			//			OnErgData(erg);
			factoryCommunication.OnErgData(erg);
			output1.text = String.Format("{0}",erg.ToString());
			//			output1.text = String.Format("We got this {0} {1}",BitConverter.IsLittleEndian,data.Length);
			BluetoothLEHardwareInterface.ReadCharacteristic (_connectedID,_serviceUUID,_characteristicUUID, actionReadErgData);
		};
	}
}
