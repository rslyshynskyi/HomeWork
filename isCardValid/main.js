function isCreditCardValid(str) {
    var cardNumber = str;

    var card = { number: cardNumber, valid: true };

    var checkForInvalidChars = function () {
        var check = /\b\d{4}-\d{4}-\d{4}-\d{4}\b/;

        if (!check.test(card.number)) {
            card.valid = false;
            card.error = 'invalid characters';
            return card;
        }

        return card
    }

    var checkNumberLength = function () {
        if(card.number.length !== 19) {
            card.valid = false;
            card.error = 'wrong_length';
            return card;
        }

        return card;
    }

    var checkForOneTypeNumber = function (arr) {
        arr.sort();
        if (arr[0] === arr[arr.length - 1]) {
            card.valid = false;
            card.error = 'only one type of number';
            return card;
        }

        return card;
    };

    var checkSumOfNumber = function (arr) {
        var sum = 0;

        for (var i = 0; i < arr.length; i++) {
            sum = sum + +arr[i];
        }

        if(sum < 16) {
            card.valid = false;
            card.error = 'sum less then 16';
            return card;
        }

        return card;
    };

    var checkFinalNumberForOdd = function (arr) {
        var finalNumber = parseInt(arr[arr.length - 1]);
        if(finalNumber % 2 !== 0) {
            card.valid = false;
            card.error = 'odd final number';
            return card;
        }

        return card;
    };

    card = checkNumberLength();
    if ( card.valid === false ) {
        return card;
    }

    card = checkForInvalidChars();
    if ( card.valid === false) {
        return card;
    }

    cardNumber = card.number.replace(/-/g, '');

    var arr = cardNumber.match(/\d/g);

    card = checkForOneTypeNumber(arr);
    if (card.valid === false) {
        return card;
    }

    card = checkSumOfNumber(arr);
    if(card.valid === false) {
        return card;
    }

    card = checkFinalNumberForOdd(arr);
    if (card.valid === false) {
        return card;
    }

    return card;
}