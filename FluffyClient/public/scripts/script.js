var ipServer = "localhost";
var wsServerUri = "ws://" + ipServer+ ":7000";
var websocket = null;
var id;

function connect() {
    websocket = new WebSocket(wsServerUri);
    websocket.onopen = function (evt) { onOpen(evt) };
    websocket.onclose = function (evt) { onClose(evt) };
    websocket.onmessage = function (evt) { onMessage(evt) };
    websocket.onerror = function (evt) { onError(evt) };

    document.getElementById("connection").remove();
    document.getElementById("creation").style.visibility = "visible";
}

function onOpen(evt) {

}

function onClose(evt) {

}

function onMessage(evt) {
    console.log("Received data: " + evt.data);
    var jobject = JSON.parse(evt.data);
    if (jobject.Command == "connected") {
        id = jobject.Id;
        console.log(id);
        document.getElementById("id").innerText = id;
    }
    else if (jobject.Command == "board") {
        boardMessage(jobject.Datas);
    }
    else if (jobject.Command == "map") {
        mapMessage(jobject.Datas);
    } else if (jobject.Command == "energy") {
        energyMessage(jobject.Datas);
    }
}

function onError(evt) {

}

function send(message) {
    websocket.send(message);
}

function start() {
    var jobject = { "Id": id, "Command": "start", "Datas": { "Width": 20, "Height": 20, "Seed": 20 } }
    var message = JSON.stringify(jobject);
    websocket.send(message);

    document.getElementById("creation").remove();
    document.getElementById("game").style.visibility = "visible";
}

function boardMessage(data) {
    var jobject = JSON.parse(data);
    var board = document.getElementById("board");
    var result = "";
    for (var i = 0; i < 3; i++)
    {
        result += "<tr>";
        for (var j = 0; j < 3; j++)
        {
            result += "<td class=\"" + jobject[i*3+j] + "\"></td>";
        }
        result += "</tr>";
    }
    board.innerHTML = result;
}

function mapMessage(data) {
    var jobject = JSON.parse(data);
    var board = document.getElementById("map");
    var result = "";
    for (var i = 0; i < 20; i++) {
        result += "<tr>";
        for (var j = 0; j < 20; j++) {
            result += "<td class=\"" + jobject[i*20 + j] + "\"></td>";
        }
        result += "</tr>";
    }
    board.innerHTML = result;
}

function energyMessage(data) {
    var energy = document.getElementById("energy");
    energy.innerText = data;
}

function onClickDirection(direction) {
    var jobject = { "Id": id, "Command": "move", "Datas": direction }
    var message = JSON.stringify(jobject);
    websocket.send(message);
}