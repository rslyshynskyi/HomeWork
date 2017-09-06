function isCreditCardValid(str) {
    var cardNumber = str;

    var card = { number: cardNumber, valid: true };

    var check = /\b\d{4}-\d{4}-\d{4}-\d{4}\b/;

    if (!check.test(card.number)) {
        card.valid = false;
        card.error = 'invalid characters';
        return card;
    }

    if (check.test(cardNumber)) {
        cardNumber = cardNumber.replace(/-/g, '');

        var arr = cardNumber.match(/\d/g);
        console.log("arr: " + arr);

        var sortArr = arr.sort();
        console.log("sortArr: " + sortArr);
        if (sortArr[0] === sortArr[sortArr.length - 1]) {
            card.valid = false;
            card.error = 'only one type of number';
            return card;
        }
        console.log("arr: " + arr);
        var sum = 0;

        for (var i = 0; i < arr.length; i++) {
            sum = sum + +arr[i];
            console.log("sum " + i + ': ' + sum);
        }

        console.log("FinSum: " + sum);

        if(sum < 16) {
            card.valid = false;
            card.error = 'sum less then 16';
            return card;
        }

        var checkOdd = cardNumber.match(/\d/g);

        var finalNumber = parseInt(checkOdd[checkOdd.length - 1]);
        console.log("FinalNumber: " + finalNumber);
        if(finalNumber % 2 !== 0) {
            card.valid = false;
            card.error = 'odd final number';
        }
    }
    else if(card.number.length !== 19) {
        card.valid = false;
        card.error = 'wrong_length';
    }
    else {
        card.valid = false;
        card.error = 'invalid characters';
    }

    return card;
}