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
    debugger;
    ctxImage.drawImage(im, 0, 0, im.width, im.height,              // source tile
        0, 0, im.width * scaleFactor, im.height * scaleFactor); // destination tile
    debugger;
    drawGridLines(gridOptions.minorLines);
    debugger;
    //drawGridLines(gridOptions.majorLines);
}

function drawGridLines(lineOptions) {
    var scaleFactor = 540 / im.width;

    var iWidth = im.width * scaleFactor;
    var iHeight = im.height * scaleFactor;

    ctxImage.strokeStyle = lineOptions.color;
    ctxImage.strokeWidth = 0.1;

    ctxImage.beginPath();

    var iCount = null;
    var i = null;
    var x = null;
    var y = null;

    iCount = Math.floor(iWidth / lineOptions.separation);

    for (i = 1; i <= iCount; i++) {
        x = (i * lineOptions.separation);
        ctxImage.moveTo(x, 0);
        ctxImage.lineTo(x, iHeight);
        ctxImage.stroke();
    }

    iCount = Math.floor(iHeight / lineOptions.separation);

    for (i = 1; i <= iCount; i++) {
        y = (i * lineOptions.separation);
        ctxImage.moveTo(0, y);
        ctxImage.lineTo(iWidth, y);
        ctxImage.stroke();
    }

    ctxImage.closePath();

    return;
}