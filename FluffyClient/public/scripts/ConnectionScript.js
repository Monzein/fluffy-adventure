var ipServer = "192.168.1.10";
var wsServerUri = "ws://" + ipServer + ":7000";
var websocket = null;
var id;

/*
    Socket Creation and use
*/
function connect() {
    websocket = new WebSocket(wsServerUri);
    websocket.onopen = function (evt) { onOpen(evt) };
    websocket.onclose = function (evt) { onClose(evt) };
    websocket.onmessage = function (evt) { onMessage(evt) };
    websocket.onerror = function (evt) { onError(evt) };

    document.getElementById("connection").remove();
    document.getElementById("game").style.visibility = "visible";
}

function onOpen(evt) {

}

function onClose(evt) {
    console.log("Socket closed!");
}

function onMessage(evt) {
    var jobject = JSON.parse(evt.data);
    if (jobject.Command == "connected") {
        id = jobject.Id;
        connectedMessage(id);
    } else if (jobject.Command == "board") {
        boardMessage(jobject.Datas);
    } else if (jobject.Command == "map") {
        mapMessage(jobject.Datas);
    } else if (jobject.Command == "info") {
        infoMessage(jobject.Datas);
    } else if (jobject.Command == "inventory") {
        inventoryMessage(jobject.Datas);
    } else if (jobject.Command == "receipes") {
        receipesMessage(jobject.Datas);
    } else if (jobject.Command == "refresh") {
        refreshMessage(jobject.Datas);
    } else if (jobject.Command == "battle") {
        battleMessage(jobject.Datas);
    }else if(jobject.Command == "end_battle"){
        endBattleMessage(jobject.Datas);
    } else if (jobject.Command == "loose"){
        looseMessage(jobject.Datas);
    } else if (jobject.Command == "equipement"){
        equipementMessage(jobject.Datas);
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

/*
    Users actions
*/

function onClickDirection(direction) {
    if(doAction()){
        var jobject = { "Id": id, "Command": "move", "Datas": direction }
        var message = JSON.stringify(jobject);
        websocket.send(message);
    }
}

function onClickInventory() {
    var jobject = { "Id": id, "Command": "inventory", "Datas": null }
    var message = JSON.stringify(jobject);
    websocket.send(message);
    jobject = { "Id": id, "Command": "equipement", "Datas": null }
    message = JSON.stringify(jobject);
    websocket.send(message);
}

function onClickMap() {
    var jobject = { "Id": id, "Command": "map", "Datas": null }
    var message = JSON.stringify(jobject);
    websocket.send(message);
}

function onClickExtract(resource) {
    if(doAction()){
        var jobject = { "Id": id, "Command": "extract", "Datas": resource }
        var message = JSON.stringify(jobject);
        websocket.send(message);
    }
}

function onClickUseItem(item) {
    var jobject = { "Id": id, "Command": "use", "Datas": item }
    var message = JSON.stringify(jobject);
    websocket.send(message);
}

function onClickEquipItem(item) {
    var jobject = { "Id": id, "Command": "equip", "Datas": item }
    var message = JSON.stringify(jobject);
    websocket.send(message);
}

function onClickUnequipItem(item) {
    var jobject = { "Id": id, "Command": "unequip", "Datas": item }
    var message = JSON.stringify(jobject);
    websocket.send(message);
}

function onClickDropItem(item) {
    var jobject = { "Id": id, "Command": "drop", "Datas": item }
    var message = JSON.stringify(jobject);
    websocket.send(message);
}

function onClickCraftItem(item) {
    var jobject = { "Id": id, "Command": "craft", "Datas": item }
    var message = JSON.stringify(jobject);
    websocket.send(message);
}

function onClickBattle(id){
    if(doAction()){
        console.log("click battle");
        var jobject = { "Id": id, "Command": "start_battle", "Datas": id }
        var message = JSON.stringify(jobject);
        websocket.send(message);
    }
}

function onClickBattleAction(action){
    if(doAction()){
        var jobject = { "Id": id, "Command": "action_battle", "Datas": action }
        var message = JSON.stringify(jobject);
        websocket.send(message);
    }
}

function onClickPick(item){
    if(doAction()){
        var jobject = { "Id": id, "Command": "pick", "Datas": item }
        var message = JSON.stringify(jobject);
        websocket.send(message);
    }
}