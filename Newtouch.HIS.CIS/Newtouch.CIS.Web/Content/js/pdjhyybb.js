

var text = "";

function bobao(text) {
	var utterThis = new window.SpeechSynthesisUtterance();
	utterThis.text = text;

	window.speechSynthesis.speak(utterThis);
	
	
}