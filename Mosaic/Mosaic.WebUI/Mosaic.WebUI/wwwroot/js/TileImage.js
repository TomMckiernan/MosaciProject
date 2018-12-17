can = document.getElementById("canvas");
var ctxImage = can.getContext("2d");

var im = new Image();

im.onload = function () {
    var tileWidth = im.width / 10;
    var tileHeight = im.height / 10;
    debugger;
    for (var i = 0; i < 20; i++) {
        for (var j = 0; j < 20; j++) {
            var x = i * 40;
            var y = j * 40;
            // image, sx, sy, sWidth, sHeight, dx, dy, dWidth, dHeight
            ctxImage.drawImage(im, x, y, 40, 40, x + i * 2, y + j * 2, 40, 40);
        }       
    }
}