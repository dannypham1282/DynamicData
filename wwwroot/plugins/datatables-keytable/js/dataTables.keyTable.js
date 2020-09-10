/*!
   Copyright 2009-2020 SpryMedia Ltd.

 This source file is free software, available under the following license:
   MIT license - http://datatables.net/license/mit

 This source file is distributed in the hope that it will be useful, but
 WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 or FITNESS FOR A PARTICULAR PURPOSE. See the license files for details.

 For details please refer to: http://www.datatables.net
 KeyTable 2.5.2
 ©2009-2020 SpryMedia Ltd - datatables.net/license
*/
var $jscomp = $jscomp || {}; $jscomp.scope = {}; $jscomp.arrayIteratorImpl = function (b) { var f = 0; return function () { return f < b.length ? { done: !1, value: b[f++] } : { done: !0 } } }; $jscomp.arrayIterator = function (b) { return { next: $jscomp.arrayIteratorImpl(b) } }; $jscomp.ASSUME_ES5 = !1; $jscomp.ASSUME_NO_NATIVE_MAP = !1; $jscomp.ASSUME_NO_NATIVE_SET = !1; $jscomp.SIMPLE_FROUND_POLYFILL = !1;
$jscomp.defineProperty = $jscomp.ASSUME_ES5 || "function" == typeof Object.defineProperties ? Object.defineProperty : function (b, f, k) { b != Array.prototype && b != Object.prototype && (b[f] = k.value) }; $jscomp.getGlobal = function (b) { b = ["object" == typeof window && window, "object" == typeof self && self, "object" == typeof global && global, b]; for (var f = 0; f < b.length; ++f) { var k = b[f]; if (k && k.Math == Math) return k } throw Error("Cannot find global object"); }; $jscomp.global = $jscomp.getGlobal(this); $jscomp.SYMBOL_PREFIX = "jscomp_symbol_";
$jscomp.initSymbol = function () { $jscomp.initSymbol = function () { }; $jscomp.global.Symbol || ($jscomp.global.Symbol = $jscomp.Symbol) }; $jscomp.SymbolClass = function (b, f) { this.$jscomp$symbol$id_ = b; $jscomp.defineProperty(this, "description", { configurable: !0, writable: !0, value: f }) }; $jscomp.SymbolClass.prototype.toString = function () { return this.$jscomp$symbol$id_ };
$jscomp.Symbol = function () { function b(k) { if (this instanceof b) throw new TypeError("Symbol is not a constructor"); return new $jscomp.SymbolClass($jscomp.SYMBOL_PREFIX + (k || "") + "_" + f++, k) } var f = 0; return b }();
$jscomp.initSymbolIterator = function () { $jscomp.initSymbol(); var b = $jscomp.global.Symbol.iterator; b || (b = $jscomp.global.Symbol.iterator = $jscomp.global.Symbol("Symbol.iterator")); "function" != typeof Array.prototype[b] && $jscomp.defineProperty(Array.prototype, b, { configurable: !0, writable: !0, value: function () { return $jscomp.iteratorPrototype($jscomp.arrayIteratorImpl(this)) } }); $jscomp.initSymbolIterator = function () { } };
$jscomp.initSymbolAsyncIterator = function () { $jscomp.initSymbol(); var b = $jscomp.global.Symbol.asyncIterator; b || (b = $jscomp.global.Symbol.asyncIterator = $jscomp.global.Symbol("Symbol.asyncIterator")); $jscomp.initSymbolAsyncIterator = function () { } }; $jscomp.iteratorPrototype = function (b) { $jscomp.initSymbolIterator(); b = { next: b }; b[$jscomp.global.Symbol.iterator] = function () { return this }; return b };
$jscomp.iteratorFromArray = function (b, f) { $jscomp.initSymbolIterator(); b instanceof String && (b += ""); var k = 0, l = { next: function () { if (k < b.length) { var g = k++; return { value: f(g, b[g]), done: !1 } } l.next = function () { return { done: !0, value: void 0 } }; return l.next() } }; l[Symbol.iterator] = function () { return l }; return l };
$jscomp.polyfill = function (b, f, k, l) { if (f) { k = $jscomp.global; b = b.split("."); for (l = 0; l < b.length - 1; l++) { var g = b[l]; g in k || (k[g] = {}); k = k[g] } b = b[b.length - 1]; l = k[b]; f = f(l); f != l && null != f && $jscomp.defineProperty(k, b, { configurable: !0, writable: !0, value: f }) } }; $jscomp.polyfill("Array.prototype.keys", function (b) { return b ? b : function () { return $jscomp.iteratorFromArray(this, function (b) { return b }) } }, "es6", "es3");
(function (b) { "function" === typeof define && define.amd ? define(["jquery", "datatables.net"], function (f) { return b(f, window, document) }) : "object" === typeof exports ? module.exports = function (f, k) { f || (f = window); k && k.fn.dataTable || (k = require("datatables.net")(f, k).$); return b(k, f, f.document) } : b(jQuery, window, document) })(function (b, f, k, l) {
    var g = b.fn.dataTable, t = 0, p = function (a, c) {
        if (!g.versionCheck || !g.versionCheck("1.10.8")) throw "KeyTable requires DataTables 1.10.8 or newer"; this.c = b.extend(!0, {}, g.defaults.keyTable,
            p.defaults, c); this.s = { dt: new g.Api(a), enable: !0, focusDraw: !1, waitingForDraw: !1, lastFocus: null, namespace: ".keyTable-" + t++, tabInput: null }; this.dom = {}; a = this.s.dt.settings()[0]; if (c = a.keytable) return c; a.keytable = this; this._constructor()
    }; b.extend(p.prototype, {
        blur: function () { this._blur() }, enable: function (a) { this.s.enable = a }, focus: function (a, c) { this._focus(this.s.dt.cell(a, c)) }, focused: function (a) { if (!this.s.lastFocus) return !1; var c = this.s.lastFocus.cell.index(); return a.row === c.row && a.column === c.column },
        _constructor: function () {
            this._tabInput(); var a = this, c = this.s.dt, d = b(c.table().node()), e = this.s.namespace, m = !1; "static" === d.css("position") && d.css("position", "relative"); b(c.table().body()).on("click" + e, "th, td", function (b) { if (!1 !== a.s.enable) { var e = c.cell(this); e.any() && a._focus(e, null, !1, b) } }); b(k).on("keydown" + e, function (b) { m || a._key(b) }); if (this.c.blurable) b(k).on("mousedown" + e, function (e) {
                b(e.target).parents(".dataTables_filter").length && a._blur(); b(e.target).parents().filter(c.table().container()).length ||
                    b(e.target).parents("div.DTE").length || b(e.target).parents("div.editor-datetime").length || b(e.target).parents().filter(".DTFC_Cloned").length || a._blur()
            }); if (this.c.editor) {
                var q = this.c.editor; q.on("open.keyTableMain", function (b, c, d) { "inline" !== c && a.s.enable && (a.enable(!1), q.one("close" + e, function () { a.enable(!0) })) }); if (this.c.editOnFocus) c.on("key-focus" + e + " key-refocus" + e, function (b, c, e, d) { a._editor(null, d, !0) }); c.on("key" + e, function (b, c, e, d, m) { a._editor(e, m, !1) }); b(c.table().body()).on("dblclick" +
                    e, "th, td", function (b) { !1 !== a.s.enable && c.cell(this).any() && a._editor(null, b, !0) }); q.on("preSubmit", function () { m = !0 }).on("preSubmitCancelled", function () { m = !1 }).on("submitComplete", function () { m = !1 })
            } if (c.settings()[0].oFeatures.bStateSave) c.on("stateSaveParams" + e, function (b, c, e) { e.keyTable = a.s.lastFocus ? a.s.lastFocus.cell.index() : null }); c.on("column-visibility" + e, function (b) { a._tabInput() }); c.on("draw" + e, function (e) {
                a._tabInput(); if (!a.s.focusDraw) {
                    var d = a.s.lastFocus; if (d && d.node && b(d.node).closest("body") ===
                        k.body) { d = a.s.lastFocus.relative; var m = c.page.info(), h = d.row + m.start; 0 !== m.recordsDisplay && (h >= m.recordsDisplay && (h = m.recordsDisplay - 1), a._focus(h, d.column, !0, e)) }
                }
            }); this.c.clipboard && this._clipboard(); c.on("destroy" + e, function () { a._blur(!0); c.off(e); b(c.table().body()).off("click" + e, "th, td").off("dblclick" + e, "th, td"); b(k).off("mousedown" + e).off("keydown" + e).off("copy" + e).off("paste" + e) }); var h = c.state.loaded(); if (h && h.keyTable) c.one("init", function () { var a = c.cell(h.keyTable); a.any() && a.focus() });
            else this.c.focus && c.cell(this.c.focus).focus()
        }, _blur: function (a) { if (this.s.enable && this.s.lastFocus) { var c = this.s.lastFocus.cell; b(c.node()).removeClass(this.c.className); this.s.lastFocus = null; a || (this._updateFixedColumns(c.index().column), this._emitEvent("key-blur", [this.s.dt, c])) } }, _clipboard: function () {
            var a = this.s.dt, c = this, d = this.s.namespace; f.getSelection && (b(k).on("copy" + d, function (a) {
                a = a.originalEvent; var b = f.getSelection().toString(), e = c.s.lastFocus; !b && e && (a.clipboardData.setData("text/plain",
                    e.cell.render(c.c.clipboardOrthogonal)), a.preventDefault())
            }), b(k).on("paste" + d, function (b) { b = b.originalEvent; var e = c.s.lastFocus, d = k.activeElement, h = c.c.editor, n; !e || d && "body" !== d.nodeName.toLowerCase() || (b.preventDefault(), f.clipboardData && f.clipboardData.getData ? n = f.clipboardData.getData("Text") : b.clipboardData && b.clipboardData.getData && (n = b.clipboardData.getData("text/plain")), h ? h.inline(e.cell.index()).set(h.displayed()[0], n).submit() : (e.cell.data(n), a.draw(!1))) }))
        }, _columns: function () {
            var a =
                this.s.dt, b = a.columns(this.c.columns).indexes(), d = []; a.columns(":visible").every(function (a) { -1 !== b.indexOf(a) && d.push(a) }); return d
        }, _editor: function (a, c, d) {
            if (this.s.lastFocus) {
                var e = this, m = this.s.dt, f = this.c.editor, h = this.s.lastFocus.cell, n = this.s.namespace; if (!(b("div.DTE", h.node()).length || null !== a && (0 <= a && 9 >= a || 11 === a || 12 === a || 14 <= a && 31 >= a || 112 <= a && 123 >= a || 127 <= a && 159 >= a))) {
                    c.stopPropagation(); 13 === a && c.preventDefault(); var g = function () {
                        f.one("open" + n, function () {
                            f.off("cancelOpen" + n); d || b("div.DTE_Field_InputControl input, div.DTE_Field_InputControl textarea").select();
                            m.keys.enable(d ? "tab-only" : "navigation-only"); m.on("key-blur.editor", function (a, b, c) { f.displayed() && c.node() === h.node() && f.submit() }); d && b(m.table().container()).addClass("dtk-focus-alt"); f.on("preSubmitCancelled" + n, function () { setTimeout(function () { e._focus(h, null, !1) }, 50) }); f.on("submitUnsuccessful" + n, function () { e._focus(h, null, !1) }); f.one("close", function () { m.keys.enable(!0); m.off("key-blur.editor"); f.off(n); b(m.table().container()).removeClass("dtk-focus-alt") })
                        }).one("cancelOpen" + n, function () { f.off(n) }).inline(h.index())
                    };
                    13 === a ? (d = !0, b(k).one("keyup", function () { g() })) : g()
                }
            }
        }, _emitEvent: function (a, c) { this.s.dt.iterator("table", function (d, e) { b(d.nTable).triggerHandler(a, c) }) }, _focus: function (a, c, d, e) {
            var m = this, g = this.s.dt, h = g.page.info(), n = this.s.lastFocus; e || (e = null); if (this.s.enable) {
                if ("number" !== typeof a) { if (!a.any()) return; var r = a.index(); c = r.column; a = g.rows({ filter: "applied", order: "applied" }).indexes().indexOf(r.row); if (0 > a) return; h.serverSide && (a += h.start) } if (-1 !== h.length && (a < h.start || a >= h.start + h.length)) this.s.focusDraw =
                    !0, this.s.waitingForDraw = !0, g.one("draw", function () { m.s.focusDraw = !1; m.s.waitingForDraw = !1; m._focus(a, c, l, e) }).page(Math.floor(a / h.length)).draw(!1); else if (-1 !== b.inArray(c, this._columns())) {
                        h.serverSide && (a -= h.start); h = g.cells(null, c, { search: "applied", order: "applied" }).flatten(); h = g.cell(h[a]); if (n) { if (n.node === h.node()) { this._emitEvent("key-refocus", [this.s.dt, h, e || null]); return } this._blur() } this._removeOtherFocus(); n = b(h.node()); n.addClass(this.c.className); this._updateFixedColumns(c); if (d ===
                            l || !0 === d) this._scroll(b(f), b(k.body), n, "offset"), d = g.table().body().parentNode, d !== g.table().header().parentNode && (d = b(d.parentNode), this._scroll(d, d, n, "position")); this.s.lastFocus = { cell: h, node: h.node(), relative: { row: g.rows({ page: "current" }).indexes().indexOf(h.index().row), column: h.index().column } }; this._emitEvent("key-focus", [this.s.dt, h, e || null]); g.state.save()
                    }
            }
        }, _key: function (a) {
            if (this.s.waitingForDraw) a.preventDefault(); else {
                var c = this.s.enable, d = !0 === c || "navigation-only" === c; if (c && (!(0 ===
                    a.keyCode || a.ctrlKey || a.metaKey || a.altKey) || a.ctrlKey && a.altKey)) {
                    var e = this.s.lastFocus; if (e) if (this.s.dt.cell(e.node).any()) {
                        e = this.s.dt; var m = this.s.dt.settings()[0].oScroll.sY ? !0 : !1; if (!this.c.keys || -1 !== b.inArray(a.keyCode, this.c.keys)) switch (a.keyCode) {
                            case 9: this._shift(a, a.shiftKey ? "left" : "right", !0); break; case 27: this.s.blurable && !0 === c && this._blur(); break; case 33: case 34: d && !m && (a.preventDefault(), e.page(33 === a.keyCode ? "previous" : "next").draw(!1)); break; case 35: case 36: d && (a.preventDefault(),
                                c = e.cells({ page: "current" }).indexes(), d = this._columns(), this._focus(e.cell(c[35 === a.keyCode ? c.length - 1 : d[0]]), null, !0, a)); break; case 37: d && this._shift(a, "left"); break; case 38: d && this._shift(a, "up"); break; case 39: d && this._shift(a, "right"); break; case 40: d && this._shift(a, "down"); break; case 113: if (this.c.editor) { this._editor(null, a, !0); break } default: !0 === c && this._emitEvent("key", [e, a.keyCode, this.s.lastFocus.cell, a])
                        }
                    } else this.s.lastFocus = null
                }
            }
        }, _removeOtherFocus: function () {
            var a = this.s.dt.table().node();
            b.fn.dataTable.tables({ api: !0 }).iterator("table", function (b) { this.table().node() !== a && this.cell.blur() })
        }, _scroll: function (a, b, d, e) { var c = d[e](), f = d.outerHeight(), h = d.outerWidth(), k = b.scrollTop(), g = b.scrollLeft(), l = a.height(); a = a.width(); "position" === e && (c.top += parseInt(d.closest("table").css("top"), 10)); c.top < k && b.scrollTop(c.top); c.left < g && b.scrollLeft(c.left); c.top + f > k + l && f < l && b.scrollTop(c.top + f - l); c.left + h > g + a && h < a && b.scrollLeft(c.left + h - a) }, _shift: function (a, c, d) {
            var e = this.s.dt, f = e.page.info(),
                k = f.recordsDisplay, h = this.s.lastFocus.cell, g = this._columns(); if (h) { var l = e.rows({ filter: "applied", order: "applied" }).indexes().indexOf(h.index().row); f.serverSide && (l += f.start); e = e.columns(g).indexes().indexOf(h.index().column); f = g[e]; "right" === c ? e >= g.length - 1 ? (l++, f = g[0]) : f = g[e + 1] : "left" === c ? 0 === e ? (l--, f = g[g.length - 1]) : f = g[e - 1] : "up" === c ? l-- : "down" === c && l++; 0 <= l && l < k && -1 !== b.inArray(f, g) ? (a && a.preventDefault(), this._focus(l, f, !0, a)) : d && this.c.blurable ? this._blur() : a && a.preventDefault() }
        }, _tabInput: function () {
            var a =
                this, c = this.s.dt, d = null !== this.c.tabIndex ? this.c.tabIndex : c.settings()[0].iTabIndex; -1 != d && (this.s.tabInput || (d = b('<div><input type="text" tabindex="' + d + '"/></div>').css({ position: "absolute", height: 1, width: 0, overflow: "hidden" }), d.children().on("focus", function (b) { var d = c.cell(":eq(0)", a._columns(), { page: "current" }); d.any() && a._focus(d, null, !0, b) }), this.s.tabInput = d), (d = this.s.dt.cell(":eq(0)", "0:visible", { page: "current", order: "current" }).node()) && b(d).prepend(this.s.tabInput))
        }, _updateFixedColumns: function (a) {
            var b =
                this.s.dt, d = b.settings()[0]; if (d._oFixedColumns) { var e = d.aoColumns.length - d._oFixedColumns.s.iRightColumns; (a < d._oFixedColumns.s.iLeftColumns || a >= e) && b.fixedColumns().update() }
        }
    }); p.defaults = { blurable: !0, className: "focus", clipboard: !0, clipboardOrthogonal: "display", columns: "", editor: null, editOnFocus: !1, focus: null, keys: null, tabIndex: null }; p.version = "2.5.2"; b.fn.dataTable.KeyTable = p; b.fn.DataTable.KeyTable = p; g.Api.register("cell.blur()", function () {
        return this.iterator("table", function (a) {
            a.keytable &&
                a.keytable.blur()
        })
    }); g.Api.register("cell().focus()", function () { return this.iterator("cell", function (a, b, d) { a.keytable && a.keytable.focus(b, d) }) }); g.Api.register("keys.disable()", function () { return this.iterator("table", function (a) { a.keytable && a.keytable.enable(!1) }) }); g.Api.register("keys.enable()", function (a) { return this.iterator("table", function (b) { b.keytable && b.keytable.enable(a === l ? !0 : a) }) }); g.Api.register("keys.move()", function (a) {
        return this.iterator("table", function (b) {
            b.keytable && b.keytable._shift(null,
                a, !1)
        })
    }); g.ext.selector.cell.push(function (a, b, d) { b = b.focused; a = a.keytable; var c = []; if (!a || b === l) return d; for (var f = 0, g = d.length; f < g; f++)(!0 === b && a.focused(d[f]) || !1 === b && !a.focused(d[f])) && c.push(d[f]); return c }); b(k).on("preInit.dt.dtk", function (a, c, d) { "dt" === a.namespace && (a = c.oInit.keys, d = g.defaults.keys, a || d) && (d = b.extend({}, d, a), !1 !== a && new p(c, d)) }); return p
});
