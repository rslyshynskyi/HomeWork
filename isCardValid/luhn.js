function isCreditCardValidByLuhn(number) {
    var numberWithoutDishes = number.replace(/-/g, '');
    var arr = numberWithoutDishes.match(/\d/g);

    arr.reverse();

    arr = firstStep(arr);

    var sumOfDigit = secondStep(arr);

    return thirdStep(sumOfDigit, number);
}

function thirdStep(sum, number) {
    var modResult = sum % 10;

    if (modResult === 0) {
        return "Credit card number(" + number + ") is valid";
    }

    return "Credit card number(" + number + ") is not valid";
}

function secondStep(arr) {
    var sum = 0;

    for (var i = 0; i < arr.length; i++) {
        sum = sum + +arr[i];
    }

    return sum;
}

function firstStep(arr) {
    for(var i = 1; i < arr.length; i = i + 2) {
        arr[i] = arr[i] * 2;
        if (arr[i] > 9) {
            arr[i] = arr[i] - 9;
        }
    }

    return arr;
}