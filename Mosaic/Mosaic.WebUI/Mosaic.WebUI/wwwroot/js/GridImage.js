can = document.getElementById("canvas");
var ctxImage = can.getContext("2d");
var im = new Image();
var showGrid = true;
var container = document.getElementById("image-container");
var currentSeparation = 10;

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

// First work out the scaling factor
// Then set the actual width and height 
// and width of the canvas from this
im.onload = function () {
    clearCanvas();
    drawImage();
    if (showGrid) {
        drawGridLines(gridOptions.minorLines);
    }
}

function clearCanvas() {
    ctxImage.clearRect(0, 0, ctxImage.width, ctxImage.height);
}

function toggleGrid() {
    showGrid = !showGrid;
    im.onload();
}

function drawResize() {
    im.onload();
}

function setSeparation(separation) {
    currentSeparation = separation;
    gridOptions.minorLines.separation = separation;
}

function getSeparation() {
    return currentSeparation;
}

function drawImage() {
    var scaleFactor = container.offsetWidth / im.width;
    var w = im.width * scaleFactor;
    var h = im.height * scaleFactor;
    can.width = w;
    can.height = h;
    ctxImage.width = w;
    ctxImage.height = h;

    ctxImage.drawImage(im, 0, 0, im.width, im.height,              // source tile
        0, 0, ctxImage.width, ctxImage.height); // destination tile
}

function drawGridLines(lineOptions) {
    var scaleFactor = container.offsetWidth / im.width;
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