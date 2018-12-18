can = document.getElementById("canvas");
var ctxImage = can.getContext("2d");

var data = '<svg width="100%" height="100%" xmlns="http://www.w3.org/2000/svg"> \
        <defs> \
            <pattern id="smallGrid" width="8" height="8" patternUnits="userSpaceOnUse"> \
                <path d="M 8 0 L 0 0 0 8" fill="none" stroke="gray" stroke-width="0.5" /> \
            </pattern> \
            <pattern id="grid" width="80" height="80" patternUnits="userSpaceOnUse"> \
                <rect width="80" height="80" fill="url(#smallGrid)" /> \
                <path d="M 80 0 L 0 0 0 80" fill="none" stroke="gray" stroke-width="1" /> \
            </pattern> \
        </defs> \
        <rect width="100%" height="100%" fill="url(#smallGrid)" /> \
    </svg>';

var DOMURL = window.URL || window.webkitURL || window;

var im = new Image();
var svg = new Blob([data], { type: 'image/svg+xml;charset=utf-8' });
var url = DOMURL.createObjectURL(svg);
debugger;
im.onload = function () {
    var scaleFactor = 540 / im.width;
    debugger;
    ctxImage.drawImage(im, 0, 0, im.width, im.height,              // source tile
        0, 0, im.width * scaleFactor, im.height * scaleFactor); // destination tile
    debugger;
    DOMURL.revokeObjectURL(url);
}