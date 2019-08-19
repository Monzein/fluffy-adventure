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
    var items = jobject[4].items.items;
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
        result += "<p>" + items[i].Name + ": " + items[i].Value + 
        " <button class=\"btn\" onclick=\"onClickPick('" + items[i].Name + "')\">Prendre</button><p>";
    }
    cellInfoView.innerHTML = result;
}

function mapMessage(data) {
    var jobject = JSON.parse(data);
    var height = jobject.height;
    var width = jobject.width;
    var map = jobject.map;
    var board = document.getElementById("allMap");
    var result = "";
    for (var i = 0; i < height; i++) {
        result += "<tr>";
        for (var j = 0; j < width; j++) {
            result += "<td class=\"allMapCell " + map[i * height + j].type + "\"></td>";
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
    var items = jobject.items;
    var result = "";
    for (i = 0; i < items.length; i++) {
        result += "<p class= \"item-cell\"" + "\";\">"

        result += "<img src=\"./images/items/generic_item.png\" class=\"item-img\"/>";
        result += "<span class=\"item-number\">" + items[i].Value + "</span>";

        result += "<img src=\"./images/actions/drop.png\" class=\"item-action-img\" onclick=\"onClickDropItem(\'"
                + items[i].Name + "\')\"/>";
        if (items[i].Usable) {
            result += "<img src=\"./images/actions/use.png\" class=\"item-action-img\" onclick=\"onClickUseItem(\'"
                + items[i].Name + "\')\"/>";
        }
        if (items[i].Equipable){
            result += "<img src=\"./images/actions/equip.png\" class=\"item-action-img\" onclick=\"onClickEquipItem(\'"
                + items[i].Name + "\')\"/>";
        }

        /*result += items[i].Name + ": " + items[i].Value;
        result += "<button type=\"button\" onclick=\"onClickDropItem(\'"
                + items[i].Name + "\')\">Lacher</button>";
        if (items[i].Usable) {
            result += "<button type=\"button\" onclick=\"onClickUseItem(\'"
                + items[i].Name + "\')\">Utiliser</button>";
        }
        if (items[i].Equipable){
            result += "<button type=\"button\" onclick=\"onClickEquipItem(\'"
                + items[i].Name + "\')\">Equiper</button>";
        }*/
        result += "</p>";
    }
    inventory.innerHTML = result;
    document.getElementById("mass").innerText = jobject.mass;
    document.getElementById("space").innerText = jobject.space;
}

function receipesMessage(data) {
    var receipes = document.getElementById("craft");
    var jobject = JSON.parse(data);
    var result = "";
    for (i = 0; i < jobject.length; i++) {
        var item = jobject[i];
        var itemName = item.Item.Name;
        result += "<p>" + itemName +
            "<button type=\"button\" onclick=\"onClickCraftItem(\'"
            + itemName + "\')\">Créer</button>" + "</p>";
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
        inBattle = false;
        $('#ModalBattle').modal('hide');
    } else{
        if(inBattle == false){
            inBattle = true;
            $('#ModalBattle').modal('show');

        }
        var jobject = JSON.parse(data);
        var attacker = jobject.attacker;
        var defender = jobject.defender;
        var firstMessage = jobject.firstMessage;
        var secondMessage = jobject.secondMessage;
        var result =  "<p>" + attacker.name + "</p><p>vie: " + attacker.health + "</p><p>Attaque</p><p>Defense</p>";
        document.getElementById("attacker").innerHTML = result;
        var result =  "<p>" + defender.name + "</p><p>vie: " + defender.health + "</p><p>Attaque</p><p>Defense</p>";
        document.getElementById("defender").innerHTML = result;
        document.getElementById("firstBattleMessage").innerHTML = firstMessage;
        document.getElementById("secondBattleMessage").innerHTML = secondMessage;

    }
}

function endBattleMessage(data){
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

function equipementMessage(data) {
    var equipement = document.getElementById("equipement");
    var jobject = JSON.parse(data);
    var resultHTML = "";
    resultHTML += "<div>Armure: " + equipementToString(jobject.Body) + "<div>";
    resultHTML += "<div>Main droite: " + equipementToString(jobject.RightHand) + "<div>";
    resultHTML += "<div>Main gauche: " + equipementToString(jobject.LeftHand) + "<div>";

    equipement.innerHTML = resultHTML;
}

function equipementToString(equipement){
    if(!equipement){
        return "Vide";
    }
    return equipement.Name + " (" + equipement.Attack + "/" + equipement.Defense + ")";
}
