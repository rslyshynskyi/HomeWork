function isCreditCardValid(str) {
    var cardNumber = str;

    var card = { number: cardNumber, valid: true };

    card = checkNumberLength(card);
    if ( card.valid === false ) {
        return card;
    }

    card = checkForInvalidChars(card);
    if ( card.valid === false) {
        return card;
    }

    cardNumber = card.number.replace(/-/g, '');

    var arr = cardNumber.match(/\d/g);

    card = checkSumOfNumber(arr, card);
    if(card.valid === false) {
        return card;
    }

    card = checkFinalNumberForOdd(arr, card);
    if (card.valid === false) {
        return card;
    }

    card = checkForOneTypeNumber(arr, card);
    if (card.valid === false) {
        return card;
    }

    return card;
}

function checkForInvalidChars (card) {
    var check = /\b\d{4}-\d{4}-\d{4}-\d{4}\b/;

    if (!check.test(card.number)) {
        card.valid = false;
        card.error = 'invalid characters';
        return card;
    }

    return card;
}

function checkNumberLength (card) {
    var totalCardNumber = 19;
    if(card.number.length !== totalCardNumber) {
        card.valid = false;
        card.error = 'wrong_length';
        return card;
    }

    return card;
}

function checkForOneTypeNumber (arr, card) {
    arr.sort();
    if (arr[0] === arr[arr.length - 1]) {
        card.valid = false;
        card.error = 'only one type of number';
        return card;
    }

    return card;
}

function checkSumOfNumber (arr, card) {
    var sum = 0;

    for (var i = 0; i < arr.length; i++) {
        sum = sum + +arr[i];
    }

    var cardNumberWithoutDishes = 16;

    if(sum < cardNumberWithoutDishes) {
        card.valid = false;
        card.error = 'sum less then 16';
        return card;
    }

    return card;
}

function checkFinalNumberForOdd (arr, card) {
    var finalNumber = parseInt(arr[arr.length - 1]);

    var divisionFlag = finalNumber % 2 === 0;

    if(!divisionFlag) {
        card.valid = false;
        card.error = 'odd final number';
        return card;
    }

    return card;
}