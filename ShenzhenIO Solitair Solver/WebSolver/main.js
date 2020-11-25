//https://stackoverflow.com/questions/9050345/selecting-last-element-in-javascript-array
if (!Array.prototype.last){
    Array.prototype.last = function(){
        return this[this.length - 1];
    };
};

function cardEditClick(event) {
    cardEdit(event, this);
}

function cardEdit(event, this1) {
    let this2 = $(this1);
    $(".selected").removeClass("selected");
    selectorEl
        .css("top", event.clientY)
        .css("left", event.clientX)
        .show()
        .focus();
    selectorEl
        .find("img[alt='" + this1.title + "']")
        .addClass("selected");
    if (this1.innerHTML != "")
        suitNumberInputEl.val(this1.innerHTML);
    this2.addClass("selected");
    selectedCardEl = this2;
}

function parseCardName(name) {
    if (name.startsWith("card-")) {
        let sep = name.lastIndexOf("-");
        let trayString = name.substring(5, sep);
        let trayIndex = parseInt(trayString);
        let indexString = name.substring(sep + 1);
        let cardIndex = parseInt(indexString);
        return { tray: trayIndex, index: cardIndex };
    } else {
        return null;
    }
}

function getCard(tray, index) {
    let name = 'card-' + tray.toString() + '-' + index.toString();
    return document.getElementById(name);
}

function getOffsetLeft(elem) {
    xPos = elem.offsetLeft;
    while (elem.offsetParent != null) {
        elem = elem.offsetParent;
        xPos += elem.offsetLeft;
    }
    return xPos;
}

function getOffsetTop(elem) {
    yPos = elem.offsetTop;
    while (elem.offsetParent != null) {
        elem = elem.offsetParent;
        yPos += elem.offsetTop;
    }
    return yPos;
}

function selectNextCard(name) {
    let elem = getCard(name.tray, name.index + 1);
    if (elem == null) {
        elem = getCard(name.tray + 1, 0);
    }
    if (elem != null) {
        let newEvent = {
            clientX: getOffsetLeft(elem),
            clientY: getOffsetTop(elem)
        }
        cardEdit(newEvent, elem);
    }
}

$(document).ready(function() {
    "use strict";
    window.traysEl = $("#trays");
    window.selectorEl = $("#selector");
    window.suitNumberInputEl = $("#suit-number");
    window.applySuitBtnEl = $("#apply-suits");
    window.solutionBoxEl = $("#solution-box");
    let addCardBtnsEl = $("#add-card-btns");
    for (var x = 0; x < 8; x++) {
        var trayEl = $('<div class="tray" id="tray-' + x + '"></div>');
        let cardId = 0;
        traysEl.append(trayEl);
        for (var y = 0; y < 5; y++) {
            let cardEl = $('<div class="card" title="" id="card-' + x + '-' + cardId++ + '"></div>');
            trayEl.append(cardEl);
        }
        addCardBtnsEl.append('<button class="add-card" data-target="tray-' + x + '">+</button>');
    }
    window.selectedCardEl = null;
    $("#trays .card").click(cardEditClick);
    $("#selector .special img").click(function () {
        let name = parseCardName(selectedCardEl[0].id);
        selectedCardEl
            .attr("class", "card " + this.alt)
            .attr("title", this.alt)
            .html("");
        selectorEl.hide();
        selectNextCard(name);
    });
    var selectedSuit = null;
    $("#selector .suits img").click(function() {
        $("#selector .suits img").removeClass("selected");
        selectedSuit = this.alt;
        $(this).addClass("selected");
        suitNumberInputEl
            .select()
            .focus();
    });
    selectorEl.keydown(function(event) {
        switch (event.key) {
            case 'g':
                $(".suits [alt*=bamboo]").click();
                event.preventDefault();
                break;
            case 'b':
                $(".suits [alt*=char]").click();
                event.preventDefault();
                break;
            case 'r':
                $(".suits [alt*=coin]").click();
                event.preventDefault();
                break;
            case 'G':
                $(".special [alt*=green]").click();
                break;
            case 'R':
                $(".special [alt*=red]").click();
                break;
            case 'W':
            case 'B':
                $(".special [alt*=white]").click();
                break;
            case 'F':
            case 'f':
                $(".special [alt*=flower]").click();
                break;
            case 'Enter':
                applySuitBtnEl.click();
                break;
            case 'Delete':
                $("#remove-card").click();
                break;
            case 'Escape':
                $("#selector .close").click();
                break;
        }
    });
    applySuitBtnEl.click(function() {
        if (selectedSuit == null)
            alert("Please select a suit");
        else {
            applySelectedCard(selectedSuit);
            selectedSuit = null;
        }
    });
    function applySelectedCard(selectedSuit) {
        let name = parseCardName(selectedCardEl[0].id);
        selectedCardEl
            .attr("class", "card " + selectedSuit)
            .attr("title", selectedSuit)
            .html(suitNumberInputEl.val());
        selectorEl.hide();
        selectNextCard(name);
    }
    $("#remove-card").click(function (ssender) {
        if (0 in selectedCardEl) {
            let name = parseCardName(selectedCardEl[0].id);
            selectedCardEl.remove();
            selectorEl.hide();

            let elem = null;
            for (let i = name.index; ; i++) {
                elem = getCard(name.tray, i + 1);
                if (elem == null) {
                    break;
                } else {
                    elem.id = 'card-' + name.tray.toString() + '-' + i.toString();
                }
            }
            name.index--;
            selectNextCard(name);
        }
    });
    $("#selector .close").click(function() {
        $(".selected").removeClass("selected");
        selectorEl.hide();
        selectedSuit = null;
    });
    $(".add-card").click(function() {
        let cardEl = $('<div class="card" title=""></div>');
        cardEl.click(cardEditClick);
        $("#" + $(this).data("target")).append(cardEl);
    });
});

function print_solution_code(solutionStates) {
    let code = "";
    for (let i = 0; i < solutionStates.length; i++) {
        let state = solutionStates[i];
        let cmd = "";
        if ("from" in state.action) {
            let val1 = state.action.from.tray;
            let val2 = state.action.FromCardIndex;
            let val3 = state.action.from.slot;
            let val4 = state.action.to.tray;
            let val5 = state.action.ToCardIndex;
            let val6 = state.action.to.slot;

            if (val1 == null) val1 = "";
            else val1 = val1.toString();

            if (val2 == null) val2 = "";
            else val2 = val2.toString();

            if (val3 == null) val3 = "";
            else val3 = val3.toString();

            if (val4 == null) val4 = "";
            else val4 = val4.toString();

            if (val5 == null) val5 = "";
            else val5 = val5.toString();

            if (val6 == null) val6 = "";
            else val6 = val6.toString();

            cmd = "M"
                + val1
                //+ state.action.from.count.toString()
                + "," + val2
                + "," + val3
                + "," + val4
                + "," + val5
                + "," + val6
        } else if ("collapse" in state.action) {
            cmd = "C" + state.action.collapse[1];
            if (cmd[1] == "W") cmd[1] = "B";
        } else if ("pop" in state.action) {
            cmd = "P" + state.action.PopSuit + state.action.pop.toString() + "," + state.action.PopCardIndex.toString(); //From Tray, from card index
        }
        code += cmd + ";";
        if ("removed" in state.action && state.action.removed.RemovedCount > 0) {
            code += "W" + state.action.removed.RemovedCount.toString();
            let suits = state.action.removed.RemovedSuitOrder;
            for (let j = 0; j < suits.length; j++) {
                code += "," + suits[j];
            }
            code += ";";
        }
    }
    console.log(code);
}

/**
 * Prepare solution to display to the view
 * @param {State[]} solutionStates
 */
function prepare_solution(solutionStates) {
    print_solution_code(solutionStates);
    $("#add-card-btns").fadeOut(300);
    function _(selector) {
        return solutionBoxEl.find(selector);
    }
    let solutionBoxPagesEl = _(".pages");
    solutionBoxEl.pages = solutionBoxPagesEl;
    solutionBoxEl.states = solutionStates;
    for (let i = 0; i < solutionStates.length; i++) {
        let state = solutionStates[i];
        let el = $("<div data-page=" + (state.step - 1) + " style='display:none'></div>");
        let text = "";
        if ("from" in state.action) {
            text = "Move "
            if ("tray" in state.action.from) {
                if (state.action.from.count > 1)
                    text += state.action.from.count + " cards ";
                else text += "a card ";
                text += "from <b>tray " + (state.action.from.tray + 1) + "</b>";
            } else {
                text += "a card from <b>slot " + (state.action.from.slot + 1) + "</b>";
            }
            text += " to ";
            if ("tray" in state.action.to)
                text += "<b>tray " + (state.action.to.tray + 1) + "</b>";
            else text += "<b>slot " + (state.action.to.slot + 1) + "</b>";
        } else if ("collapse" in state.action) {
            text = "Collapse ";
            switch (state.action.collapse) {
                case "DG":
                    text += "<b>Green Dragon</b>";
                    break;
                case "DR":
                    text += "<b>Red Dragon</b>";
                    break;
                case "DW":
                    text += "<b>White Dragon</b>";
                    break;
            }
        } else if ("pop" in state.action) {
            text = "Remove one card from <b>tray " + (state.action.pop + 1) + "</b>";
        } else {
            text = "?";
        }
        el.html(text);
        solutionBoxPagesEl.append(el);
    }
    let finalPage = $("<div data-page=" + solutionStates.length + " style='display:none'></div>")
    finalPage.html("Solved! :D");
    solutionBoxPagesEl.append(finalPage);
    
    solutionBoxEl.currentPageEl = _(".current-page");
    solutionBoxEl.currentPage = 0;
    solutionBoxEl.maxPage = solutionStates.length;
    solutionBoxEl.next_page = function() {
        if (this.currentPage < this.maxPage)
            this.open_step(++this.currentPage);
    };
    solutionBoxEl.prev_page = function() {
        if (this.currentPage > 0)
            this.open_step(--this.currentPage);
    };
    solutionBoxEl.open_step = function(num) {
        this.pages.children().fadeOut(200);
        this.pages.find("[data-page=" + num + "]")
            .delay(200)
            .fadeIn(200);
        this.currentPageEl.text(this.currentPage + 1);
        if (num >= this.states.length) { //if opening final page
            $("#slots, #trays").fadeOut(200);
            return;
        } else $("#slots, #trays").fadeIn(200);
        let state = this.states[num];
        load_from_json(state.prevState.trays);
        for (let i = 0; i < 3; i++) {
            if (state.prevState.slots[i] == null) {
                $("#slots #slot-" + i).html('<div class="card"></div>');
                continue;
            }
            let el = generate_card_element(state.prevState.slots[i], false);
            $("#slots #slot-" + i).html(el);
        }
        let action = state.action;
        $("#trays .selected-target").removeClass("selected-target");
        if ("from" in action) {
            if ("tray" in action.from)
                $("#trays #tray-" + action.from.tray + " :nth-last-child(-n + " + action.from.count + ")")
                    .addClass("selected-from");
            else if ("slot" in action.from)
                $("#slots #slot-" + action.from.slot + " .card")
                    .addClass("selected-from");
            if ("tray" in action.to)
                $("#trays #tray-" + action.to.tray + " :last-child, #trays #tray-" + action.to.tray)
                    .addClass("selected-target");
            else if ("slot" in action.to)
                $("#slots #slot-" + action.to.slot + " .card")
                    .addClass("selected-target");
        } else if ("collapse" in action) {
            let target = ".card.";
            switch (action.collapse) {
                case "DW":
                    target += "dragon.white";
                    break;
                case "DR":
                    target += "dragon.red";
                    break;
                case "DG":
                    target += "dragon.green";
                    break;
            }
            $(target).addClass("selected-from");
        } else if ("pop" in action) {
            $("#tray-" + action.pop + " :last-child")
                .addClass("selected-from");
        }
    };
    _(".current-page").text(1);
    _(".total-page").text(solutionBoxEl.maxPage + 1);
    _(".pages > :first-child").show();
    _(".navigation .next-page").click(function() {
        solutionBoxEl.next_page();
    });
    _(".navigation .prev-page").click(function() {
        solutionBoxEl.prev_page();
    });
    $("#solve-btn, #help-btn").fadeOut(200, function() {
        solutionBoxEl.fadeIn(200);
    })
    solutionBoxEl.open_step(0);
    $("#slots").fadeIn(300);
}

/**
 * Return array of cards from selected cards
 * @return {Array}
 */
function load_from_dom() {
    var trays = [];
    var traysEls = window.traysEl.children();
    for (var i = 0; i < trays_count; i++) {
        var tray = [];
        var cardsEl = traysEls[i].children;
        for (var j = 0; j < cardsEl.length; j++) {
            var cardEl = cardsEl[j];
            var tags = cardEl.title.split(' ');
            switch (tags[0]) {
                case 'flower':
                    tray.push('F');
                    continue;
                case 'dragon':
                    switch (tags[1]) {
                        case 'green':
                            tray.push(['D', 'G']);
                            continue;
                        case 'red':
                            tray.push(['D', 'R']);
                            continue;
                        case 'white':
                            tray.push(['D', 'W']);
                            continue;
                    }
                case 'suit':
                    switch (tags[1]) {
                        case 'bamboo':
                            tray.push(['G', parseInt(cardEl.textContent)]);
                            continue;
                        case 'char':
                            tray.push(['B', parseInt(cardEl.textContent)]);
                            continue;
                        case 'coin':
                            tray.push(['R', parseInt(cardEl.textContent)]);
                            continue;
                    }
            }
        }
        trays.push(tray);
    }
    //console.log(trays);
    return trays;
}

/**
 * Load cards from json or array instead of manually selecting cards
 * (you can use converter.js to convert cards from http://tgratzer.com/shenzhen-solitaire into json)
 * @param {String|Array} json 
 */
function load_from_json(json) {
    if (json[0] == '[')
        json = JSON.parse(json);
    var traysEls = window.traysEl.children();
    for (var i = 0; i < trays_count; i++) {
        traysEls[i].innerHTML = "";
        for (var j = 0; j < json[i].length; j++) {
            let cardEl = generate_card_element(json[i][j]);
            traysEls[i].innerHTML += cardEl;
        }
        
    }
}

/**
 * Generate element string from given card array
 * @param {Array} card
 * @param {Boolean} flower should flower returned?
 */
function generate_card_element(card, flower = true) {
    let tags = "";
    let content = "";
    switch (card[0]) {
        case "F":
            if (flower)
                tags = "flower";
            else tags = "full";
            break;
        case "D":
            switch (card[1]) {
                case "R":
                    tags = "dragon red";
                    break;
                case "G":
                    tags = "dragon green";
                    break;
                case "W":
                    tags = "dragon white";
                    break;
            }
            break;
        case "R":
            content = card[1];
            tags = "suit coin";
            break;
        case "G":
            content = card[1];
            tags = "suit bamboo";
            break;
        case "B":
            content = card[1];
            tags = "suit char";
            break;
    }
    return '<div class="card ' + tags + '" title="' + tags + '">' + content + '</div>';
}

function help() {
    alert(
        "There is some keyboard shortcuts you can use, when you select the card, the selector dialog will pop up. In this dialog, you can press :\n\n"+
        "Shift + G => Green Dragon\n" +
        "Shift + R => Red Dragon\n" +
        "Shift + B or Shift + W => White Dragon\n" +
        "F => Flower\n" +
        "G then [number] then ENTER => Bamboo card with value [number]\n" +
        "B then [number] then ENTER => Char card with value [number]\n" +
        "R then [number] then ENTER => Coin card with value [number]\n" +
        "Delete => Remove the selected card\n" +
        "Escape => Close the selector dialog and unselect current card\n" +
        "\n" +
        "Don't forget to turn the Caps Lock off otherwise you may have some problems with shortcut :D"
    );
}