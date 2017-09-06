var parser = {
    parse: function (inputStr) {
        var delSpace = /\s/g;

        var findErrorExp = /[-+*/]\s*[+*/]/;
        var findLeftBracket = /\(/;
        var findRightBracket = /\)/;

        if(findErrorExp.test(inputStr)) {
            return undefined;
        }

        var findBracketsExpression = /\((-?\d+(?:\.\d+)?)\s*([-+*\/])\s*(-?\d+(?:\.\d+)?)\)/;

        var findMulExp = /(-?\d+(?:\.\d+)?)\s*([*])\s*(-?\d+(?:\.\d+)?)/;
        var findDivExp = /(-?\d+(?:\.\d+)?)\s*([\/])\s*(-?\d+(?:\.\d+)?)/;
        var findAddExp = /(-?\d+(?:\.\d+)?)\s*([\+])\s*(-?\d+(?:\.\d+)?)/;
        var findSubExp = /(-?\d+(?:\.\d+)?)\s*([-])\s*(-?\d+(?:\.\d+)?)/;

        var resultStr = inputStr.replace(delSpace, "");
        var tempStr;

        if(findBracketsExpression.test(resultStr)) {
            var tempStr =  parser.calculatePart(resultStr, findBracketsExpression);
            resultStr = resultStr.replace(findBracketsExpression, tempStr);
        }

        if (findLeftBracket.test(resultStr) || findRightBracket.test(resultStr)) {
            return undefined;
        }

        while (tempStr = findMulExp.exec(resultStr)) {
            tempStr =  parser.calculatePart(resultStr, findMulExp);
            resultStr = resultStr.replace(findMulExp, tempStr);
        }

        while (tempStr = findDivExp.exec(resultStr)) {
            tempStr =  parser.calculatePart(resultStr, findDivExp);
            resultStr = resultStr.replace(findDivExp, tempStr);
        }

        while (tempStr = findAddExp.exec(resultStr)) {
            tempStr =  parser.calculatePart(resultStr, findAddExp);
            resultStr = resultStr.replace(findAddExp, tempStr);
        }

        while (tempStr = findSubExp.exec(resultStr)) {
            tempStr =  parser.calculatePart(resultStr, findSubExp);
            resultStr = resultStr.replace(findSubExp, tempStr);
        }

        return parseFloat(resultStr);
    },
    add: function (first, second) {
        return parseFloat(first)  + parseFloat(second);
    },
    sub: function (first, second) {
        return parseFloat(first) - parseFloat(second);
    },
    mul: function (first, second) {
        return Math.round((parseFloat(first) * parseFloat(second)) * 100) / 100;
    },
    div: function (first, second) {
        return Math.round((parseFloat(first) / parseFloat(second)) * 100) / 100;
    },
    calculatePart: function (strPart, reg) {
        var arr = strPart.match(reg);
        var result;
        switch (arr[2]) {
            case '+':
                result = parser.add(arr[1], arr[3]);
                break;
            case '-':
                result = parser.sub(arr[1], arr[3]);
                break;
            case '*':
                result = parser.mul(arr[1], arr[3]);
                break;
            case '/':
                result = parser.div(arr[1], arr[3]);
                break;
        }

        return result;
    }
}

module.exports = parser;