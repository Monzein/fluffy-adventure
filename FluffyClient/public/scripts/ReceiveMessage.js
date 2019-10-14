/*
    Messages received are applied to the view here.
*/
var playerId;
var canAction = true;
var inBattle = false;

var lastCell = "";
var lastCharacters = "";
var lastItems = "";

function connectedMessage(data){
    playerId = "joueur_" + data;
    document.getElementById("id").innerText = data;
}

function boardMessage(data) {
    
    var jobject = JSON.parse(data);
    var position = jobject.x + ";" + jobject.y;
    var cells = jobject.cells;
    var board = document.getElementById("board");
    var result = "";

    var info = cells[4];

    var ressources = cells[4].ressources;
    if(position!=lastCell){
        for (var i = 0; i < 3; i++) {
            result += "<tr>";
            for (var j = 0; j < 3; j++) {
                result += "<td class=\"mapCell " + cells[i * 3 + j].type + "\"></td>";
            }
            result += "</tr>";
        }
        board.innerHTML = result;
        lastCell = position;
        document.getElementById("cellInfo-type").innerHTML = info.type;

        result = "";
        for (i = 0; i < ressources.length; i++) {
            result += "<p><button type=\"button\" class=\"btn btn-default\" onclick=\"onClickExtract('" +
                ressources[i].name + "')\">Extraire</button>" +
                ressources[i].name + ": <span id=\"resources-" + ressources[i].name + "\">" + ressources[i].value + "<span></p>";
        }
        document.getElementById("cellInfo-resources").innerHTML= result;
    }else{
        for (i = 0; i < ressources.length; i++) {
            document.getElementById("resources-"+ressources[i].name).innerHTML= ressources[i].value;
        }
    }

    var characters = cells[4].characters;
    var items = cells[4].items.items;

    result = "";
    for (i = 0; i < characters.length; i++) {
        if(playerId != characters[i].Name){
            result += "<p>" + characters[i].Name +
            " <button type=\"button\" class=\"btn\" onclick=\"onClickBattle('" +characters[i].Id + "')\">"
            +"<span class=\"glyphicon glyphicon-screenshot\" </span></button></p>";
        }
    }
    if(lastCharacters!=result){
        document.getElementById("cellInfo-characters").innerHTML = result;
        lastCharacters = result;
    }

    result = "";
    for (i = 0; i < items.length; i++) {
        result += "<p>" + items[i].Name + ": " + items[i].Value + 
        " <button type=\"button\" class=\"btn\" onclick=\"onClickPick('" + items[i].Name + "')\">Prendre</button></p>";
    }
    if(lastItems!=result){
        document.getElementById("cellInfo-items").innerHTML = result;
        lastItems = result;
    }
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
        result += "<div class= \"item-cell\">";

        result += "<img src=\"./images/items/generic_item.png\" class=\"item-img\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\""+ items[i].Name + "\"/>";

        result += "<img src=\"./images/actions/drop.png\" class=\"item-action-img\" onclick=\"onClickDropItem(\'"
                + items[i].Name + "\')\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Lâcher\"/>";
        if (items[i].Usable) {
            result += "<img src=\"./images/actions/use.png\" class=\"item-action-img\" onclick=\"onClickUseItem(\'"
                + items[i].Name + "\')\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Utiliser\"/>";
        }   
        else if (items[i].Equipable){
            result += "<img src=\"./images/actions/equip.png\" class=\"item-action-img\" onclick=\"onClickEquipItem(\'"
                + items[i].Name + "\')\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Equiper\"/>";
        } else {
            result += "<div class=\"item-action-img\"></div>";
        }

        result += "<span class=\"item-number\">" + items[i].Value + "</span>";

        result += "</div>";
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
        result += "<div>" + itemName +
            " <button class=\"btn btn-small\" type=\"button\" onclick=\"onClickCraftItem(\'"
            + itemName + "\')\">Créer</button> ";
        result += "<button class=\"btn btn-small\" type=\"button\" data-toggle=\"collapse\" data-target=\"#ingredients-" +
        i + "\" aria-expanded=\"false\" aria-controls=\"ingredients-" + i + "\">Ingredients</button>";
        result += "<div class=\"collapse\" id=\"ingredients-" + i + "\"><div class=\"card card-body\">";

        for(j = 0; j < item.Ingredients.length; j++){
            var ingredient = item.Ingredients[j];
            result += "<p>" + ingredient.Number + " " + ingredient.Name + "</p>";
        }
        result += "</div></div>";
        result += "</div>";
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
    var jobject = JSON.parse(data);

    console.log(jobject);
    document.getElementById("attack").innerHTML = jobject.Attack;
    document.getElementById("defense").innerHTML = jobject.Defense;

    var equipements = jobject.Equipements;
    document.getElementById("body-slot").innerHTML = equipementToString(equipements.Body,"body");
    document.getElementById("right-slot").innerHTML = equipementToString(equipements.RightHand,"right");
    document.getElementById("left-slot").innerHTML = equipementToString(equipements.LeftHand,"left");
}

function equipementToString(equipement, slot){
    
    var result = "";
    if(!equipement){
        result = "<img src=\"./images/equipements/" + slot + "_slot.png\" class=\"item-img\"/>";
        result += "<img src=\"./images/actions/emptyAction.png\" class=\"item-action-img\"/>";
    }
    else{
        result += "<img src=\"./images/items/generic_item.png\" class=\"item-img\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"";
        result += equipement.Name + " (" + equipement.Attack + "/" + equipement.Defense + ")";
        result += "\"/>";
        result += "<img src=\"./images/actions/drop.png\" class=\"item-action-img\" onclick=\"onClickUnequipItem(\'"
        + equipement.Name + "\')\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Retirer\"/>";
    }

    return result;
}
