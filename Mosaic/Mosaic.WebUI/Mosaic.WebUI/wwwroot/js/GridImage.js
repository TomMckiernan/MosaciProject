can = document.getElementById("canvas");
var ctxImage = can.getContext("2d");
var im = new Image();

var gridOptions = {
    minorLines: {
        separation: 10,
        color: '#FFFFFF'
    },
    majorLines: {
        separation: 30,
        color: '#FF0000'
    }
};

im.onload = function () {
    var scaleFactor = 540 / im.width;
    ctxImage.width = im.width * scaleFactor;
    ctxImage.height = im.height * scaleFactor;
    ctxImage.drawImage(im, 0, 0, im.width, im.height,              // source tile
                       0, 0, ctxImage.width, ctxImage.height); // destination tile
    drawGridLines(gridOptions.minorLines);
    //drawGridLines(gridOptions.majorLines);
}

function clearCanvas() {
    debugger;
    ctxImage.clearRect(0, 0, ctxImage.width, ctxImage.height);
}

function drawGridLines(lineOptions) {
    var scaleFactor = 540 / im.width;

    var iWidth = im.width * scaleFactor;
    var iHeight = im.height * scaleFactor;
    var scaleSeparation = lineOptions.separation * scaleFactor;

    ctxImage.strokeStyle = lineOptions.color;
    ctxImage.strokeWidth = 0.1;

    ctxImage.beginPath();

    var iCount = null;
    var i = null;
    var x = null;
    var y = null;

    iCount = Math.floor(iWidth / scaleSeparation);

    for (i = 1; i <= iCount; i++) {
        x = (i * scaleSeparation);
        ctxImage.moveTo(x, 0);
        ctxImage.lineTo(x, iHeight);
        ctxImage.stroke();
    }

    iCount = Math.floor(iHeight / scaleSeparation);

    for (i = 1; i <= iCount; i++) {
        y = (i * scaleSeparation);
        ctxImage.moveTo(0, y);
        ctxImage.lineTo(iWidth, y);
        ctxImage.stroke();
    }

    ctxImage.closePath();

    return;
}