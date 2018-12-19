can = document.getElementById("canvas");
var ctxImage = can.getContext("2d");
var im = new Image();

im.onload = function () {
    var scaleFactor = 540 / im.width;
    debugger;
    var tileWidth = im.width / 10;
    var tileHeight = im.height / 10;
    debugger;
    for (var i = 0; i * 40 < im.width; i++) {
        for (var j = 0; j * 40 < im.height; j++) {
            var x = i * 40;
            var y = j * 40;
            // image, sx, sy, sWidth, sHeight, dx, dy, dWidth, dHeight
            ctxImage.drawImage(im, x, y, 40, 40,              // source tile
                (x + i * 5) * scaleFactor, (y + j * 5) * scaleFactor, 40 * scaleFactor, 40 * scaleFactor); // destination tile
        }       
    }
    debugger;
}