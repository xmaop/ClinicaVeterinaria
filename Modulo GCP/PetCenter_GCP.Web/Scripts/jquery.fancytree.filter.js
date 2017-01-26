/*!
 * jquery.fancytree.filter.js
 *
 * Remove or highlight tree nodes, based on a filter.
 * (Extension module for jquery.fancytree.js: https://github.com/mar10/fancytree/)
 *
 * Copyright (c) 2014, Martin Wendt (http://wwWendt.de)
 *
 * Released under the MIT license
 * https://github.com/mar10/fancytree/wiki/LicenseInfo
 *
 * @version @VERSION
 * @date @DATE
 */

; (function ($, window, document, undefined) {

    "use strict";


    /*******************************************************************************
     * Private functions and variables
     */

    function _escapeRegex(str) {
        /* jshint regexdash:true */
        return (str + "").replace(/([.?*+\^\$\[\]\\(){}|-])/g, "\\$1");
    }

    $.ui.fancytree._FancytreeClass.prototype._applyFilterImpl = function (filter, branchMode, leavesOnly) {
        var match, re,
            count = 0,
            hideMode = this.options.filter.mode === "hide";
        // leavesOnly = !branchMode && this.options.filter.leavesOnly;
        leavesOnly = !!leavesOnly && !branchMode;

        // Default to 'match title substring (not case sensitive)'
        if (typeof filter === "string") {
            match = _escapeRegex(filter); // make sure a '.' is treated literally
            re = new RegExp(".*" + match + ".*", "i");
            filter = function (node) {
                return !!re.exec(node.title);
            };
        }

        this.enableFilter = true;
        this.lastFilterArgs = arguments;

        this.$div.addClass("fancytree-ext-filter");
        if (hideMode) {
            this.$div.addClass("fancytree-ext-filter-hide");
        } else {
            this.$div.addClass("fancytree-ext-filter-dimm");
        }
        // Reset current filter
        this.visit(function (node) {
            delete node.match;
            delete node.subMatch;
        });
        // Adjust node.hide, .match, .subMatch flags
        this.visit(function (node) {
            if ((!leavesOnly || node.children == null) && filter(node)) {
                count++;
                node.match = true;
                node.visitParents(function (p) {
                    p.subMatch = true;
                });
                if (branchMode) {
                    node.visit(function (p) {
                        p.match = true;
                    });
                    return "skip";
                }
            }
        });
        // Redraw
        this.render();
        return count;
    };

    $.ui.fancytree._FancytreeClass.prototype._applyFilterImplToSelected = function (branchMode, leavesOnly) {
        var count = 0,
         hideMode = this.options.filter.mode === "hide";
        leavesOnly = !!leavesOnly && !branchMode;
        var filter = "";
        filter = function (node) {
            return node.partsel;
        };

        this.enableFilter = true;
        this.$div.addClass("fancytree-ext-filter");
        if (hideMode) {
            this.$div.addClass("fancytree-ext-filter-hide");
        } else {
            this.$div.addClass("fancytree-ext-filter-dimm");
        }
        // Reset current filter
        this.visit(function (node) {
            delete node.match;
            delete node.subMatch;
        });
        // Adjust node.hide, .match, .subMatch flags
        this.visit(function (node) {
            if ((!leavesOnly || node.children == null) && filter(node)) {
                count++;
                node.match = true;
                node.visitParents(function (p) {
                    p.subMatch = true;
                });
                if (branchMode) {
                    node.visit(function (p) {
                        p.match = true;
                    });
                    return "skip";
                }
            }
        });
        // Redraw
        this.render();
        return count;

    }

    $.ui.fancytree._FancytreeClass.prototype._applyFilterPublicationCustom = function (branchMode, leavesOnly, properties) {
        //debugger;
        var count = 0,
        leavesOnly = !!leavesOnly && !branchMode;

        var arraySearch = filter;

        var filter = function (node) {
            var estado = false;
            estado = node[properties[0]] > 0;
            return estado;
        };

        this.enableFilter = true;
        this.$div.addClass("fancytree-ext-filter");
        this.$div.addClass("fancytree-ext-filter-hide");

        this.visit(function (node) {
            delete node.match;
            delete node.subMatch;
        });

        this.visit(function (node) {
            if (node.children == null && filter(node)) {
                count++;
                node.match = true;
                node.visitParents(function (p) {
                    p.subMatch = true;
                });
                node.visit(function (p) {
                    p.match = true;
                });
                return "skip";

            }
        });
        this.render();
        return count;
    };



    $.ui.fancytree._FancytreeClass.prototype._applyFilterAlertCustom = function (branchMode, leavesOnly, properties) {
        var count = 0,
        leavesOnly = !!leavesOnly && !branchMode;

        var arraySearch = filter;

        var filter = function (node) {
            var estado = false;
            estado = node[properties[0]] > 0;
            return estado;
        };

        this.enableFilter = true;
        this.$div.addClass("fancytree-ext-filter");
        this.$div.addClass("fancytree-ext-filter-hide");

        this.visit(function (node) {
            delete node.match;
            delete node.subMatch;
        });
        
        this.visit(function (node) {        
            if (node.children == null && filter(node)) {
                count++;
                node.match = true;
                node.visitParents(function (p) {
                    p.subMatch = true;
                });                
                node.visit(function (p) {
                    p.match = true;
                });
                return "skip";
                
            }
        });
        this.render();
        return count;
    };


    $.ui.fancytree._FancytreeClass.prototype._applyFilterImplCustom = function (filter, branchMode, leavesOnly, properties) {

        var count = 0,
            hideMode = this.options.filter.mode === "hide";
        leavesOnly = !!leavesOnly && !branchMode;

        var arraySearch = filter;

        filter = function (node) {
            var estado = false;
            if (properties.length == 1)
                estado = !!new RegExp(".*" + _escapeRegex(arraySearch[0]) + ".*", "i").exec(node[properties[0]]);
            else if (properties.length == 2)
                estado = !!new RegExp(".*" + _escapeRegex(arraySearch[0]) + ".*", "i").exec(node[properties[0]]) && !!new RegExp(".*" + _escapeRegex(arraySearch[1]) + ".*", "i").exec(node[properties[1]]);
            else if (properties.length == 3)
                estado = !!new RegExp(".*" + _escapeRegex(arraySearch[0]) + ".*", "i").exec(node[properties[0]]) && !!new RegExp(".*" + _escapeRegex(arraySearch[1]) + ".*", "i").exec(node[properties[1]]) && !!new RegExp(".*" + _escapeRegex(arraySearch[2]) + ".*", "i").exec(node[properties[2]]);
                /* Fix Temporal */
            else if (properties.length == 4) {
                estado = !!new RegExp(".*" + _escapeRegex(arraySearch[0]) + ".*", "i").exec(node[properties[0]]) && !!new RegExp(".*" + _escapeRegex(arraySearch[1]) + ".*", "i").exec(node[properties[1]]) && !!new RegExp(".*" + _escapeRegex(arraySearch[2]) + ".*", "i").exec(node[properties[2]]);
                if (estado)
                    estado = arraySearch[3] != "" ? (parseInt(node[properties[3]]) <= parseInt(arraySearch[3]) ? true : false) : estado;
            }
            /* Fin */
            return estado;
        };

        this.enableFilter = true;
        this.$div.addClass("fancytree-ext-filter");
        if (hideMode) {
            this.$div.addClass("fancytree-ext-filter-hide");
        } else {
            this.$div.addClass("fancytree-ext-filter-dimm");
        }
        // Reset current filter
        this.visit(function (node) {
            delete node.match;
            delete node.subMatch;
        });
        // Adjust node.hide, .match, .subMatch flags

        this.visit(function (node) {
            if ((!leavesOnly || node.children == null) && filter(node)) {
                count++;
                node.match = true;
                node.visitParents(function (p) {
                    p.subMatch = true;
                });
                if (branchMode) {
                    node.visit(function (p) {
                        p.match = true;
                    });
                    return "skip";
                }
            }
        });
        // Redraw
        this.render();
        return count;
    };

    /**
     * [ext-filter] Dimm or hide nodes.
     *
     * @param {function | string} filter
     * @param {boolean} [leavesOnly=false]
     * @returns {integer} count
     * @alias Fancytree#filterNodes
     * @requires jquery.fancytree.filter.js
     */
    $.ui.fancytree._FancytreeClass.prototype.filterNodes = function (filter, leavesOnly, searchBy) {
        return this._applyFilterImpl(filter, false, leavesOnly, searchBy);
    };


    $.ui.fancytree._FancytreeClass.prototype.filterNodesCustom = function (filter, leavesOnly, searchBy) {
        return this._applyFilterImplCustom(filter, false, leavesOnly, searchBy);
    };

    $.ui.fancytree._FancytreeClass.prototype.filterAlertCustom = function (leavesOnly, searchBy) {
        return this._applyFilterAlertCustom(false, leavesOnly, searchBy);
    };

    $.ui.fancytree._FancytreeClass.prototype.filterPublicationCustom = function (leavesOnly, searchBy) {
        return this._applyFilterPublicationCustom(false, leavesOnly, searchBy);
    };

    $.ui.fancytree._FancytreeClass.prototype.filterSelected = function () {
        return this._applyFilterImplToSelected(false, false);
    };

    $.ui.fancytree._FancytreeClass.prototype.applyFilter = function (filter) {
        this.warn("Fancytree.applyFilter() is deprecated since 2014-05-10. Use .filterNodes() instead.");
        return this.filterNodes.apply(this, arguments);
    };

    /**
     * [ext-filter] Dimm or hide whole branches.
     *
     * @param {function | string} filter
     * @returns {integer} count
     * @alias Fancytree#filterBranches
     * @requires jquery.fancytree.filter.js
     */
    $.ui.fancytree._FancytreeClass.prototype.filterBranches = function (filter) {
        return this._applyFilterImpl(filter, true, null);
    };


    /**
     * [ext-filter] Reset the filter.
     *
     * @alias Fancytree#clearFilter
     * @requires jquery.fancytree.filter.js
     */
    $.ui.fancytree._FancytreeClass.prototype.clearFilter = function () {
        this.visit(function (node) {
            delete node.match;
            delete node.subMatch;
        });
        this.enableFilter = false;
        this.$div.removeClass("fancytree-ext-filter fancytree-ext-filter-dimm fancytree-ext-filter-hide");
        this.render();
    };


    /*******************************************************************************
     * Extension code
     */
    $.ui.fancytree.registerExtension({
        name: "filter",
        version: "0.2.0",
        // Default options for this extension.
        options: {
            mode: "dimm"
            //		leavesOnly: false
        },
        treeInit: function (ctx) {
            this._super(ctx);
        },
        nodeRenderStatus: function (ctx) {
            // Set classes for current status
            var res,
                node = ctx.node,
                tree = ctx.tree,
                $span = $(node[tree.statusClassPropName]);

            res = this._super(ctx);
            // nothing to do, if node was not yet rendered
            if (!$span.length || !tree.enableFilter) {
                return res;
            }
            $span
                .toggleClass("fancytree-match", !!node.match)
                .toggleClass("fancytree-submatch", !!node.subMatch)
                .toggleClass("fancytree-hide", !(node.match || node.subMatch));

            return res;
        }
    });
}(jQuery, window, document));
