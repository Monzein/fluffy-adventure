/*
    Messages received are applied to the view here.
*/
var playerId;
var canAction = true;
var inBattle = false;

function connectedMessage(data){
    playerId = "joueur_" + data;
    document.getElementById("id").innerText = data;
}

function boardMessage(data) {
    var jobject = JSON.parse(data);
    var board = document.getElementById("board");
    var result = "";
    for (var i = 0; i < 3; i++) {
        result += "<tr>";
        for (var j = 0; j < 3; j++) {
            result += "<td class=\"mapCell " + jobject[i * 3 + j].type + "\"></td>";
        }
        result += "</tr>";
    }
    board.innerHTML = result;
    var info = jobject[4];
    var ressources = jobject[4].ressources;
    var characters = jobject[4].characters;
    var items = jobject[4].items;
    var cellInfoView = document.getElementById("cellInfo");
    result = "<p>" + info.type + "</p><p>Ressources : </p>";
    for (i = 0; i < ressources.length; i++) {
        result += "<p><button class=\"btn btn-default\" onclick=\"onClickExtract('" +
            ressources[i].name + "')\">Extraire</button>" +
            ressources[i].name + ": " + ressources[i].value + "<p>";
    }
    result += "</p><p>Personnages : </p>";
    for (i = 0; i < characters.length; i++) {
        if(playerId != characters[i].Name){
            result += "<p>" + characters[i].Name +
            " <button class=\"btn\" onclick=\"onClickBattle('" +characters[i].Id + "')\">"
            +"<span class=\"glyphicon glyphicon-screenshot\" </span></button><p>";
            //"')\" data-toggle=\"modal\" data-target=\"#ModalBattle\" data-backdrop=\"static\" data-keyboard=\"false\"></span><p>";
        }
    }
    result += "</p><p>Objets : </p>";
    for (i = 0; i < items.length; i++) {
        result += "<p>" + items[i].Name + ": " + items[i].Value + "<p>";
    }
    cellInfoView.innerHTML = result;
}

function mapMessage(data) {
    var jobject = JSON.parse(data);
    var board = document.getElementById("allMap");
    var result = "";
    for (var i = 0; i < 50; i++) {
        result += "<tr>";
        for (var j = 0; j < 50; j++) {
            result += "<td class=\"allMapCell " + jobject[i * 50 + j].type + "\"></td>";
        }
        result += "</tr>";
    }
    board.innerHTML = result;
}

function infoMessage(data) {
    var jobject = JSON.parse(data);
    document.getElementById("energy").innerText = jobject.energy + "/" + jobject.maxEnergy;
    document.getElementById("health").innerText = jobject.health + "/" + jobject.maxHealth;
}

function inventoryMessage(data) {
    var inventory = document.getElementById("inventory");
    var jobject = JSON.parse(data);
    var result = "";
    for (i = 0; i < jobject.length; i++) {
        result += "<p>" + jobject[i].Name + ": " + jobject[i].Value;
        if (jobject[i].Usable) {
            result += "<button type=\"button\" onclick=\"onClickUseItem(\'"
                + jobject[i].Name + "\')\">Utiliser</button>";
        }
        result += "</p>";
    }
    inventory.innerHTML = result;
}

function receipesMessage(data) {
    var receipes = document.getElementById("craft");
    var jobject = JSON.parse(data);
    var result = "";
    for (i = 0; i < jobject.length; i++) {
        result += "<p>" + jobject[i] +
            "<button type=\"button\" onclick=\"onClickCraftItem(\'"
            + jobject[i] + "\')\">Créer</button>" + "</p>";
    }
    receipes.innerHTML = result;
}

function refreshMessage(data) {
    canAction = true;
    var timer = document.getElementById("timer");
    timer.innerHTML = "<span class=\"glyphicon glyphicon-ok\" style=\"\"></span>";
}

function battleMessage(data) {
    if(data == null){
        console.log("no battle")
        inBattle = false;
        $('#ModalBattle').modal('hide');
    } else{
        if(inBattle == false){
            console.log("start battle")
            inBattle = true;
            $('#ModalBattle').modal('show');

        }
        var jobject = JSON.parse(data);
        var attacker = jobject.attacker;
        var defender = jobject.defender;
        var result =  "<p>" + attacker.name + "</p><p>vie: " + attacker.health + "</p><p>Attaque</p><p>Defense</p>";
        document.getElementById("attacker").innerHTML = result;
        var result =  "<p>" + defender.name + "</p><p>vie: " + defender.health + "</p><p>Attaque</p><p>Defense</p>";
        document.getElementById("defender").innerHTML = result;
    }
}

function endBattleMessage(data){
    console.log("end battle")
    inBattle = false;
    $('#ModalBattle').modal('hide');
}

function looseMessage(data){
    document.getElementById("game").innerHTML = "Vous êtes mort!";
}

function doAction(){
    if(canAction){
        canAction = false;
        var timer = document.getElementById("timer");
        timer.innerHTML = "<span class=\"glyphicon glyphicon-time\" style=\"\"></span>";
        return true;
    }
    return false;
}

