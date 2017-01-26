var handleSlimScroll = function () {
        "use strict";
        $("[data-scrollbar=true]").each(function () {
            generateSlimScroll($(this))
        })
    },
    mensaje = function (a, b, c,d) {
        if (!b) return;
        $.gritter.add({
            title: a,
            text: b,
            sticky: false,
            image: c=='' ? '' : '/assets/img/mensaje/' + c + '.png',
            class_name: 'gritter-' + d,
            before_open: function () {
                if ($(".gritter-item-wrapper").length === 6) {
                    return false
                }
            }
        });
    },
    restaFechas = function (f1, f2) {
        f1 = f1 + ""; f2 = f2 + "";
        var aFecha1 = f1.split('/');
        var aFecha2 = f2.split('/');
        var fFecha1 = Date.UTC(aFecha1[2], aFecha1[1] - 1, aFecha1[0]);
        var fFecha2 = Date.UTC(aFecha2[2], aFecha2[1] - 1, aFecha2[0]);
        var dif = fFecha2 - fFecha1;
        var dias = Math.floor(dif / (1000 * 60 * 60 * 24)) + 1;
        return dias;
    }
    generateSlimScroll = function (e) {
        if (!$(e).attr("data-init")) {
            var a = $(e).attr("data-height");
            a = a ? a : $(e).height();
            var t = {
                height: a,
                alwaysVisible: !0
            };
            /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) ? ($(e).css("height", a), $(e).css("overflow-x", "scroll")) : $(e).slimScroll(t), $(e).attr("data-init", !0)
        }
    },
    handleSidebarMenu = function () {
        "use strict";
        $(".sidebar .nav > .has-sub > a").click(function () {
            var e = $(this).next(".sub-menu"),
                a = ".sidebar .nav > li.has-sub > .sub-menu";
            0 === $(".page-sidebar-minified").length && ($(a).not(e).slideUp(250, function () {
                $(this).closest("li").removeClass("expand")
            }), $(e).slideToggle(250, function () {
                var e = $(this).closest("li");
                $(e).hasClass("expand") ? $(e).removeClass("expand") : $(e).addClass("expand")
            }))
        }), $(".sidebar .nav > .has-sub .sub-menu li.has-sub > a").click(function () {
            if (0 === $(".page-sidebar-minified").length) {
                var e = $(this).next(".sub-menu");
                $(e).slideToggle(250)
            }
        })
    },
    handleMobileSidebarToggle = function () {
        var e = !1;
        $(".sidebar").bind("click touchstart", function (a) {
            0 !== $(a.target).closest(".sidebar").length ? e = !0 : (e = !1, a.stopPropagation())
        }), $(document).bind("click touchstart", function (a) {
            0 === $(a.target).closest(".sidebar").length && (e = !1), a.isPropagationStopped() || e === !0 || ($("#page-container").hasClass("page-sidebar-toggled") && (e = !0, $("#page-container").removeClass("page-sidebar-toggled")), $(window).width() <= 767 && $("#page-container").hasClass("page-right-sidebar-toggled") && (e = !0, $("#page-container").removeClass("page-right-sidebar-toggled")))
        }), $("[data-click=right-sidebar-toggled]").click(function (a) {
            a.stopPropagation();
            var t = "#page-container",
                i = "page-right-sidebar-collapsed";
            i = $(window).width() < 979 ? "page-right-sidebar-toggled" : i, $(t).hasClass(i) ? $(t).removeClass(i) : e !== !0 ? $(t).addClass(i) : e = !1, $(window).width() < 480 && $("#page-container").removeClass("page-sidebar-toggled"), $(window).trigger("resize")
        }), $("[data-click=sidebar-toggled]").click(function (a) {
            a.stopPropagation();
            var t = "page-sidebar-toggled",
                i = "#page-container";
            $(i).hasClass(t) ? $(i).removeClass(t) : e !== !0 ? $(i).addClass(t) : e = !1, $(window).width() < 480 && $("#page-container").removeClass("page-right-sidebar-toggled")
        })
    },
    handleSidebarMinify = function () {
        $("[data-click=sidebar-minify]").click(function (e) {
            e.preventDefault();
            var a = "page-sidebar-minified",
                t = "#page-container";
            $('#sidebar [data-scrollbar="true"]').css("margin-top", "0"), $('#sidebar [data-scrollbar="true"]').removeAttr("data-init"), $("#sidebar [data-scrollbar=true]").stop(), $(t).hasClass(a) ? ($(t).removeClass(a), $(t).hasClass("page-sidebar-fixed") ? (0 !== $("#sidebar .slimScrollDiv").length && ($('#sidebar [data-scrollbar="true"]').slimScroll({
                destroy: !0
            }), $('#sidebar [data-scrollbar="true"]').removeAttr("style")), generateSlimScroll($('#sidebar [data-scrollbar="true"]')), $("#sidebar [data-scrollbar=true]").trigger("mouseover")) : /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) && (0 !== $("#sidebar .slimScrollDiv").length && ($('#sidebar [data-scrollbar="true"]').slimScroll({
                destroy: !0
            }), $('#sidebar [data-scrollbar="true"]').removeAttr("style")), generateSlimScroll($('#sidebar [data-scrollbar="true"]')))) : ($(t).addClass(a), /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) ? ($('#sidebar [data-scrollbar="true"]').css("margin-top", "0"), $('#sidebar [data-scrollbar="true"]').css("overflow", "visible")) : ($(t).hasClass("page-sidebar-fixed") && ($('#sidebar [data-scrollbar="true"]').slimScroll({
                destroy: !0
            }), $('#sidebar [data-scrollbar="true"]').removeAttr("style")), $("#sidebar [data-scrollbar=true]").trigger("mouseover"))), $(window).trigger("resize")
        })
    },
    handlePageContentView = function () {
        "use strict";
        $.when($("#page-loader").addClass("hide")).done(function () {
            $("#page-container").addClass("in")
        })
    },
    panelActionRunning = !1,
    handlePanelAction = function () {
        //"use strict";
        return panelActionRunning ? !1 : (panelActionRunning = !0, $(document).on("hover", "[data-click=panel-remove]", function (e) {
            $(this).tooltip({
                title: "Remove",
                placement: "bottom",
                trigger: "hover",
                container: "body"
            }), $(this).tooltip("show")
        }), $(document).on("click", "[data-click=panel-remove]", function (e) {
            e.preventDefault(), $(this).tooltip("destroy"), $(this).closest(".panel").remove()
        }), $(document).on("hover", "[data-click=panel-collapse]", function (e) {
            $(this).tooltip({
                title: "Collapse / Expand",
                placement: "bottom",
                trigger: "hover",
                container: "body"
            }), $(this).tooltip("show")
        }), $(document).on("click", "[data-click=panel-collapse]", function (e) {
            e.preventDefault(), $(this).closest(".panel").find(".panel-body").slideToggle()
        }), $(document).on("hover", "[data-click=panel-reload]", function (e) {
            $(this).tooltip({
                title: "Reload",
                placement: "bottom",
                trigger: "hover",
                container: "body"
            }), $(this).tooltip("show")
        }), $(document).on("click", "[data-click=panel-reload]", function (e) {
            e.preventDefault();
            var a = $(this).closest(".panel");
            if (!$(a).hasClass("panel-loading")) {
                var t = $(a).find(".panel-body"),
                    i = '<div class="panel-loader"><span class="spinner-small"></span></div>';
                $(a).addClass("panel-loading"), $(t).prepend(i), setTimeout(function () {
                    $(a).removeClass("panel-loading"), $(a).find(".panel-loader").remove()
                }, 2e3)
            }
        }), $(document).on("hover", "[data-click=panel-expand]", function (e) {
            $(this).tooltip({
                title: "Expand / Compress",
                placement: "bottom",
                trigger: "hover",
                container: "body"
            }), $(this).tooltip("show")
        }), void $(document).on("click", "[data-click=panel-expand]", function (e) {
            e.preventDefault();
            var a = $(this).closest(".panel"),
                t = $(a).find(".panel-body"),
                i = 40;
            if (0 !== $(t).length) {
                var n = $(a).offset().top,
                    o = $(t).offset().top;
                i = o - n
            }
            if ($("body").hasClass("panel-expand") && $(a).hasClass("panel-expand")) $("body, .panel").removeClass("panel-expand"), $(".panel").removeAttr("style"), $(t).removeAttr("style");
            else if ($("body").addClass("panel-expand"), $(this).closest(".panel").addClass("panel-expand"), 0 !== $(t).length && 40 != i) {
                var s = 40;
                $(a).find(" > *").each(function () {
                    var e = $(this).attr("class");
                    "panel-heading" != e && "panel-body" != e && (s += $(this).height() + 30)
                }), 40 != s && $(t).css("top", s + "px")
            }
            $(window).trigger("resize")
        }))
    },
    handelTooltipPopoverActivation = function () {
        "use strict";
        $("[data-toggle=tooltip]").tooltip(), $("[data-toggle=popover]").popover()
    },
    handleScrollToTopButton = function () {
        "use strict";
        $(document).scroll(function () {
            var e = $(document).scrollTop();
            e >= 200 ? $("[data-click=scroll-top]").addClass("in") : $("[data-click=scroll-top]").removeClass("in")
        }), $("[data-click=scroll-top]").click(function (e) {
            e.preventDefault(), $("html, body").animate({
                scrollTop: $("body").offset().top
            }, 500)
        })
    },
    handleThemePanelExpand = function () {
        $(document).on("click", '[data-click="theme-panel-expand"]', function () {
            var e = ".theme-panel",
                a = "active";
            $(e).hasClass(a) ? $(e).removeClass(a) : $(e).addClass(a)
        })
    },
    handleSavePanelPosition = function (e) {
        "use strict";
        if (0 !== $(".ui-sortable").length) {
            var a = [],
                t = 0;
            $.when($(".ui-sortable").each(function () {
                var e = $(this).find("[data-sortable-id]");
                if (0 !== e.length) {
                    var i = [];
                    $(e).each(function () {
                        var e = $(this).attr("data-sortable-id");
                        i.push({
                            id: e
                        })
                    }), a.push(i)
                } else a.push([]);
                t++
            })).done(function () {
                var t = window.location.href;
                t = t.split("?"), t = t[0], localStorage.setItem(t, JSON.stringify(a)), $(e).find('[data-id="title-spinner"]').delay(500).fadeOut(500, function () {
                    $(this).remove()
                })
            })
        }
    },
    handleIEFullHeightContent = function () {
        var e = window.navigator.userAgent,
            a = e.indexOf("MSIE ");
        (a > 0 || navigator.userAgent.match(/Trident.*rv\:11\./)) && $('.vertical-box-row [data-scrollbar="true"][data-height="100%"]').each(function () {
            var e = $(this).closest(".vertical-box-row"),
                a = $(e).height();
            $(e).find(".vertical-box-cell").height(a)
        })
    },
    handleMobileSidebar = function () {
        "use strict";
        /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) && $("#page-container").hasClass("page-sidebar-minified") && ($('#sidebar [data-scrollbar="true"]').css("overflow", "visible"), $('.page-sidebar-minified #sidebar [data-scrollbar="true"]').slimScroll({
            destroy: !0
        }), $('.page-sidebar-minified #sidebar [data-scrollbar="true"]').removeAttr("style"), $(".page-sidebar-minified #sidebar [data-scrollbar=true]").trigger("mouseover"));
        var e = 0;
        $(".page-sidebar-minified .sidebar [data-scrollbar=true] a").bind("touchstart", function (a) {
            var t = a.originalEvent.touches[0] || a.originalEvent.changedTouches[0],
                i = t.pageY;
            e = i - parseInt($(this).closest("[data-scrollbar=true]").css("margin-top"))
        }), $(".page-sidebar-minified .sidebar [data-scrollbar=true] a").bind("touchmove", function (a) {
            if (a.preventDefault(), /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
                var t = a.originalEvent.touches[0] || a.originalEvent.changedTouches[0],
                    i = t.pageY,
                    n = i - e;
                $(this).closest("[data-scrollbar=true]").css("margin-top", n + "px")
            }
        }), $(".page-sidebar-minified .sidebar [data-scrollbar=true] a").bind("touchend", function (a) {
            var t = $(this).closest("[data-scrollbar=true]"),
                i = $(window).height(),
                n = parseInt($("#sidebar").css("padding-top")),
                o = $("#sidebar").height();
            e = $(t).css("margin-top");
            var s = n;
            $(".sidebar").not(".sidebar-right").find(".nav").each(function () {
                s += $(this).height()
            });
            var l = -parseInt(e) + $(".sidebar").height();
            if (l >= s && s >= i && s >= o) {
                var r = i - s - 20;
                $(t).animate({
                    marginTop: r + "px"
                })
            } else parseInt(e) >= 0 || o >= s ? $(t).animate({
                marginTop: "0px"
            }) : (r = e, $(t).animate({
                marginTop: r + "px"
            }))
        })
    },
    
    handleClearSidebarSelection = function () {
        $(".sidebar .nav > li, .sidebar .nav .sub-menu").removeClass("expand").removeAttr("style")
    },
    handleClearSidebarMobileSelection = function () {
        $("#page-container").removeClass("page-sidebar-toggled")
    },
    App = function () {
        //"use strict";
        return {
            init: function () {
                this.initSidebar(), this.initPageLoad(), this.initComponent(), this.initThemePanel()
            },
            initSidebar: function () {
                handleSidebarMenu(), handleMobileSidebarToggle(), handleSidebarMinify(), handleMobileSidebar()
            },
            initSidebarSelection: function () {
                handleClearSidebarSelection()
            },
            initSidebarMobileSelection: function () {
                handleClearSidebarMobileSelection()
            },
            initPageLoad: function () {
                handlePageContentView()
            },
            initComponent: function () {
                mensaje(), restaFechas(), handleIEFullHeightContent(), handleSlimScroll(), handlePanelAction(), handelTooltipPopoverActivation(), handleScrollToTopButton()
            },
            initThemePanel: function () {
                handleThemePanelExpand()
            },
            scrollTop: function () {
                $("html, body").animate({
                    scrollTop: $("body").offset().top
                }, 0)
            }
        }
    }();