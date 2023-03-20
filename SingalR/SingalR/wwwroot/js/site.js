function bounceElements(selector) {
    function randomInt(min, max) {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }

    const html = document.querySelector("html")
    const elements = [];

    document.querySelectorAll(selector).forEach(element => {
        element.style.top = `${randomInt(0, html.offsetHeight - element.offsetHeight - 1)}px`;
        element.style.left = `${randomInt(0, html.offsetWidth - element.offsetWidth - 1)}px`;
        element.style.position = 'absolute';

        elements.push({
            element: element,
            topIncrease: randomInt(1, 3),
            leftIncrease: randomInt(1, 3),
        })
    })

    function move_block(element) {
        let maxHeight = html.offsetHeight - 1;
        let maxWidth = html.offsetWidth - 1;

        let topLocation = element.element.offsetTop;
        let bottomLocation = topLocation + element.element.offsetHeight;

        let leftLocation = element.element.offsetLeft;
        let rightLocation = leftLocation + element.element.offsetWidth;

        //
        // switch directions when border is reached
        //
        if (bottomLocation >= maxHeight) {
            element.topIncrease = randomInt(-3, -1);
        } else if (topLocation <= 0) {
            element.topIncrease = randomInt(1, 3);
        }

        if (rightLocation >= maxWidth) {
            element.leftIncrease = randomInt(-3, -1);
        } else if (leftLocation <= 0) {
            element.leftIncrease = randomInt(1, 3);
        }

        //
        // prevent overflow on borders
        //
        if (element.topIncrease > maxHeight - bottomLocation) {
            element.topIncrease = maxHeight - bottomLocation;
        } else if (element.topIncrease < 0 && Math.abs(element.topIncrease) > topLocation) {
            element.topIncrease = -(topLocation);
        }

        if (element.leftIncrease > maxWidth - rightLocation) {
            element.leftIncrease = maxWidth - rightLocation;
        } else if (element.leftIncrease < 0 && Math.abs(element.leftIncrease) > leftLocation) {
            element.leftIncrease = -(leftLocation);
        }

        element.element.style.top = `${topLocation + element.topIncrease}px`;
        element.element.style.left = `${leftLocation + element.leftIncrease}px`;
    }

    setInterval(function () {
        elements.forEach(elementData => {
            move_block(elementData);
        })
    }, 1);
}


bounceElements('.canvas');