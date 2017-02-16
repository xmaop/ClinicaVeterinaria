$(document).ready(function () {

    if ($.fn.reflect) {
        $('#preview-coverflow .cover').reflect(); // only possible in very specific situations
    }

    $('#preview-coverflow').coverflow({
        index: 6,
        density: 2,
        innerOffset: 50,
        innerScale: .7,
        animateStep: function (event, cover, offset, isVisible, isMiddle, sin, cos) {
            if (isVisible) {
                if (isMiddle) {
                    $(cover).css({
                        'filter': 'none',
                        '-webkit-filter': 'none'
                    });
                } else {
                    var brightness = 1 + Math.abs(sin),
										contrast = 1 - Math.abs(sin),
										filter = 'contrast(' + contrast + ') brightness(' + brightness + ')';
                    $(cover).css({
                        'filter': filter,
                        '-webkit-filter': filter
                    });
                }
            }
        }
    });
    function tabsToSpaces(line, tabsize) {
        var out = '',
						tabsize = tabsize || 4,
						c;
        for (c in line) {
            var ch = line.charAt(c);
            if (ch === '\t') {
                do {
                    out += ' ';
                } while (out.length % tabsize);
            } else {
                out += ch;
            }
        }
        return out;
    }

    function visualizeElement(element, type) {
        var code = $(element).html().split('\n'),
						tabsize = 4,
						minlength = 2E53,
						l;

        // Convert tabs to spaces
        for (l in code) {
            code[l] = tabsToSpaces(code[l], tabsize);
        }


        // determine minimum length
        var minlength = 2E53;
        var first = 2E53;
        var last = 0;
        for (l in code) {
            if (/\S/.test(code[l])) {
                minlength = Math.min(minlength, /^\s*/.exec(code[l])[0].length);
                first = Math.min(first, l);
                last = Math.max(last, l);
            }
        }

        code = code.slice(first, last + 1);

        // strip tabs at start
        for (l in code) {
            code[l] = code[l].slice(minlength);
        }

        // recombine
        code = code.join('\n');

        var fragment = $('<pre class="prettyprint"><code/></pre>').text(code).insertAfter(element);
        $('<h3 class="clickable">' + type + '&hellip;</h3>').insertBefore(fragment).click(function () {
            fragment.slideToggle();
        });
    }

    // extract html fragments
    $('div.prettyprint, span.prettyprint').each(function () {
        visualizeElement(this, 'HTML');
    });

    // extract scripts
    $('script.prettyprint').each(function () {
        visualizeElement(this, 'Javascript');
    });

});