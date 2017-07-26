
class ManageBufferPM5C2Erg{
	private byte[] data;
	private int index;
	private ManageBufferPM5C2Erg(byte[] data){
		CheckArchitecture(data);
		this.data = data;
		index = 0;
	}
	private void CheckArchitecture(byte[] data){
		if (BitConverter.IsLittleEndian == false)
			Array.Reverse(data); // Convert big endian to little endian
	}

	private float read3Bytes(float multiplier) {
		int v0 = data[index];
		int v1 = data[index+1];
		int v2 = data[index+2];
		index = index + 3;
		return (v0 | v1 <<8 |  v2 << 16)*multiplier;
	};
	private float read2Bytes(float multiplier) {
		float r = BitConverter.ToUInt16(data)*multiplier;
		index +=2;
		return r;
	};
	private float readPace() {
		return this.read2Bytes(0.01);
	}	
	private float readTime(){
		return this.read3Bytes(0.01);
	}

	private float readDistance(){
		return this.read3Bytes(0.1);
	}

	private float readUnimportantFlags(int nFlags){
		this.index = this.index + nFlags;
		return 0;
	}
	private byte readByte(){
		// this.index = this.index + 1;
		return data[index++];
	}


	public static void unPackageErgEntry31(string characteristic,byte[] data) {
		ManageBufferPM5C2Erg bufferErg = new ManageBufferPM5C2Erg(data);
		float time = bufferErg.readTime();
		float distance = bufferErg.readDistance ();
		float flags = bufferErg.readUnimportantFlags(5);
		float totalWOGDistance = bufferErg.readDistance();
		float totalWOGTime =bufferErg.readTime();
		byte WOGTimeType =bufferErg.readByte();
		byte drag = bufferErg.readByte();
		BluetoothLEHardwareInterface.Log ("received data: " + characteristic);
		BluetoothLEHardwareInterface.Log (String.Format("received data: {0} {1} {2}",time,distance,drag));
	}
	public static void unPackageErgEntry32(string characteristic,byte[] data){
		ManageBufferPM5C2Erg bufferErg = new ManageBufferPM5C2Erg(data);
		float time  = bufferErg.readTime();
		float speed = bufferErg.read2Bytes(0.001f);
		float spm = bufferErg.readByte();
		float hr = bufferErg.readByte();
		float pace = bufferErg.readPace();
		float avgPace = bufferErg.readPace();
		float restDistance = bufferErg.read2Bytes(1.0f);
		float restTime =bufferErg.readTime();
		BluetoothLEHardwareInterface.Log ("received data: " + characteristic);
		BluetoothLEHardwareInterface.Log (String.Format("received data: {0} {1} {2}",time,spm,pace));
	}
	public static void unPackageErgEntry33(string characteristic,byte[] data){
		ManageBufferPM5C2Erg bufferErg = new ManageBufferPM5C2Erg(data);
		float time =  bufferErg.readTime();
		float interval = bufferErg.readByte();
		float avgPower = bufferErg.read2Bytes(1.0f);
		float totalCalories = bufferErg.read2Bytes(1.0f);
		float spAvgPace = bufferErg.readPace();
		float spAvgPower = bufferErg.read2Bytes(1.0f);
		float spAvgCalories = bufferErg.read2Bytes(1.0f);
		float spAvgTime = bufferErg.read3Bytes(0.1f);
		float spAvgDistance = bufferErg.readDistance();
		BluetoothLEHardwareInterface.Log (String.Format("received data: {0} {1} ",time,avgPower));
	}
	public static void unPackageErgEntry35(string characteristic,byte[] data){
		ManageBufferPM5C2Erg bufferErg = new ManageBufferPM5C2Erg(data);
		float time  = bufferErg.readTime();
		float distance = bufferErg.readDistance();
		float driveLength = bufferErg.readByte()*0.001f;
		float driveTime = bufferErg.readByte()*0.01f;
		float strokeRecoveryTime = bufferErg.read2Bytes(0.01f);
		float strokeRecoveryDistance = bufferErg.read2Bytes(0.01f);
		float peakDriveForce =bufferErg.read2Bytes(0.1f);
		float avgDriveForce =bufferErg.read2Bytes(0.1f);
		float strokeCount =bufferErg.read2Bytes(1.0f);
		BluetoothLEHardwareInterface.Log ("received data: " + characteristic);
		BluetoothLEHardwareInterface.Log (String.Format("received data: {0} {1} {2}",time,driveTime,strokeRecoveryTime));
	}
	public static void unPackageErgEntry3A(string characteristic,byte[] data){
		ManageBufferPM5C2Erg bufferErg = new ManageBufferPM5C2Erg(data);

		float garbage = bufferErg.readUnimportantFlags(8);
		float calories = bufferErg.read2Bytes(1.0f);
		float power = bufferErg.read2Bytes(1.0f);

		BluetoothLEHardwareInterface.Log ("received data: " + characteristic);
		BluetoothLEHardwareInterface.Log (String.Format("received data: {0} {1} ",calories,power));
	}

	public static void unPackageErgEntryGeneric(string characteristic,byte[] data) {
		BluetoothLEHardwareInterface.Log ("Error Generic: " + characteristic);
		BluetoothLEHardwareInterface.Log ("received data: " + characteristic);
		BluetoothLEHardwareInterface.Log ("received data: " + bytes);
	}

}
