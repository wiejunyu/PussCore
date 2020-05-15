//(function($) {
//  'use strict';

//  $(function() {
//    var $fullText = $('.admin-fullText');
//    $('#admin-fullscreen').on('click', function() {
//      $.AMUI.fullscreen.toggle();
//    });

//    $(document).on($.AMUI.fullscreen.raw.fullscreenchange, function() {
//      $fullText.text($.AMUI.fullscreen.isFullscreen ? '退出全屏' : '开启全屏');
//    });


//    var getWindowHeight = $(window).height(),
//        myappLoginBg    = $('.myapp-login-bg');
//    myappLoginBg.css('min-height',getWindowHeight + 'px');
//  });
//})(jQuery);

//获取url中的参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}

function GetAjaxUrl() {
    return "https://118.89.182.215";
}

function GUID() {
    this.date = new Date();   /* 判断是否初始化过，如果初始化过以下代码，则以下代码将不再执行，实际中只执行一次 */
    if (typeof this.newGUID != 'function') {   /* 生成GUID码 */
        GUID.prototype.newGUID = function () {
            this.date = new Date(); var guidStr = '';
            sexadecimalDate = this.hexadecimal(this.getGUIDDate(), 16);
            sexadecimalTime = this.hexadecimal(this.getGUIDTime(), 16);
            for (var i = 0; i < 9; i++) {
                guidStr += Math.floor(Math.random() * 16).toString(16);
            }
            guidStr += sexadecimalDate;
            guidStr += sexadecimalTime;
            while (guidStr.length < 32) {
                guidStr += Math.floor(Math.random() * 16).toString(16);
            }
            return this.formatGUID(guidStr);
        }
        /* * 功能：获取当前日期的GUID格式，即8位数的日期：19700101 * 返回值：返回GUID日期格式的字条串 */
        GUID.prototype.getGUIDDate = function () {
            return this.date.getFullYear() + this.addZero(this.date.getMonth() + 1) + this.addZero(this.date.getDay());
        }
        /* * 功能：获取当前时间的GUID格式，即8位数的时间，包括毫秒，毫秒为2位数：12300933 * 返回值：返回GUID日期格式的字条串 */
        GUID.prototype.getGUIDTime = function () {
            return this.addZero(this.date.getHours()) + this.addZero(this.date.getMinutes()) + this.addZero(this.date.getSeconds()) + this.addZero(parseInt(this.date.getMilliseconds() / 10));
        }
        /* * 功能: 为一位数的正整数前面添加0，如果是可以转成非NaN数字的字符串也可以实现 * 参数: 参数表示准备再前面添加0的数字或可以转换成数字的字符串 * 返回值: 如果符合条件，返回添加0后的字条串类型，否则返回自身的字符串 */
        GUID.prototype.addZero = function (num) {
            if (Number(num).toString() != 'NaN' && num >= 0 && num < 10) {
                return '0' + Math.floor(num);
            } else {
                return num.toString();
            }
        }
        /*  * 功能：将y进制的数值，转换为x进制的数值 * 参数：第1个参数表示欲转换的数值；第2个参数表示欲转换的进制；第3个参数可选，表示当前的进制数，如不写则为10 * 返回值：返回转换后的字符串 */GUID.prototype.hexadecimal = function (num, x, y) {
            if (y != undefined) { return parseInt(num.toString(), y).toString(x); }
            else { return parseInt(num.toString()).toString(x); }
        }
        /* * 功能：格式化32位的字符串为GUID模式的字符串 * 参数：第1个参数表示32位的字符串 * 返回值：标准GUID格式的字符串 */
        GUID.prototype.formatGUID = function (guidStr) {
            var str1 = guidStr.slice(0, 8) + '-', str2 = guidStr.slice(8, 12) + '-', str3 = guidStr.slice(12, 16) + '-', str4 = guidStr.slice(16, 20) + '-', str5 = guidStr.slice(20);
            return str1 + str2 + str3 + str4 + str5;
        }
    }
}

/**
 * Created by MingChen on 2016/11/3.
 */
function sourceController() {
    this.root = "";

    this.callfunc = null; // 回调函数

    this.css = [];

    this.script = [];

    // css数量，兼容safari用
    var cssCount = document.styleSheets.length;

    // timeOut
    var tmpTimeOut = [];

    // 时间戳
    var startTime = null;

    /**
     * 设置文件根目录
     * @param url 根目录地址
     */
    this.setRoot = function (url) {
        this.root = url;
    };

    /**
     * 设置回调函数
     * @param func 回调函数
     */
    this.setCallBack = function (func) {
        this.callfunc = func;
    };

    /** 添加Script文件 请在addSource钱调用
     * @param url script文件路径
     */
    this.addScript = function (url) {
        if (url === "" || url == null) return;
        this.script.push(url);
    };

    /**
     * 添加CSS文件 请在addSource钱调用
     * @param url css文件路径
     */
    this.addCss = function (url) {
        if (url === "" || url == null) return;
        // console.log("csslength:" + this.css.length);
        this.css.push(url);
    };

    /** 添加常用资源 */
    this.addSource = function () {
        addMeta([["name", "renderer"], ["content", "ie-comp"]]);
        createCss(this);
    };

    /**
     * 添加<link> 默认先加载css
     * @param obj 当前对象
     */
    function createCss(obj) {
        // 如果没有css文件，不加载
        if (obj.css.length <= 0) return;
        var url = obj.css.shift();
        // console.log(url);
        var css = document.createElement("link");
        css.setAttribute("rel", "stylesheet");
        if (obj.root != "") {
            url = obj.root + "/" + url;
        }
        css.href = url;

        // 老版本safari特殊处理，获取浏览器信息的方法请自己添加
        if ($.browser == "safari" && parseInt(browserInfo().version) < 6) {
            var timer = setInterval(function () {
                // console.log("timer cssCount:" + cssCount + " len:" + document.styleSheets.length);
                clearTmpTimeOut();
                if (document.styleSheets.length == cssCount && obj.css.length > 0) {
                    addTask(createCss, 0, obj);
                } else if (document.styleSheets.length == cssCount) {
                    // 如果有js文件，开始加载js
                    if (obj.script.length > 0) {
                        addTask(createJs, 0, obj);
                    } else {
                        obj.css.length = 0;
                        if (typeof obj.callfunc == "function") {
                            obj.callfunc();
                        }
                    }
                    clearInterval(timer);
                }
            }, 50);
        } else {
            css.onload = function () {
                clearTmpTimeOut();
                if (obj.css.length > 0) {
                    addTask(createCss, 0, obj);
                } else {
                    // 如果有js文件，开始加载js
                    if (obj.script.length > 0) {
                        addTask(createJs, 0, obj);
                    } else {
                        tmpTimeOut.length = 0;
                        obj.css.length = 0;
                        if (typeof obj.callfunc == "function") {
                            obj.callfunc();
                        }
                    }
                }
            };
        }
        document.getElementsByTagName("head")[0].appendChild(css);
        cssCount++;
    }

    /**
     * 添加<script> js加载完后执行回调函数
     * @param obj 当前对象
     */
    function createJs(obj) {
        // 如果没有js文件，不加载
        if (obj.script.length <= 0) return;
        $.getScript((obj.root) ? (obj.root + "/" + obj.script.shift()) : (obj.script.shift()));
        var script = document.createElement('script');
        //script.setAttribute('type', 'text/javascript');
        //script.setAttribute('src', (obj.root) ? (obj.root + "/" + obj.script.shift()) : (obj.script.shift()));
        script.onload = script.onreadystatechange = function () {
            if (script.ready) {
                return false;
            }
            if (!script.readyState || script.readyState == "loaded" || script.readyState == 'complete') {
                script.onload = script.onreadystatechange = null;
                script.ready = true;
                clearTmpTimeOut();
                // console.log("time：" + (new Date().getTime() - startTime) + "ms " + script.getAttribute("src"));

                if (obj.script.length > 0) {
                    addTask(createJs, 0, obj);
                } else {
                    tmpTimeOut.length = 0;
                    obj.script.length = 0;
                    // 如果有回调函数 执行回调函数
                    if (typeof obj.callfunc == "function") {
                        obj.callfunc();
                    }
                }
            }
        };
        startTime = new Date().getTime();
        document.getElementsByTagName("body")[0].appendChild(script);
    }

    /**
     *添加一个任务，
     *    @param {Function} fun 任务函数名
     *    @param {number} delay 定时时间
     *    @param {object} params 传递到fun中的参数
     */
    function addTask(fun, delay) {
        var to = null;
        if (typeof fun == 'function') {
            var argu = Array.prototype.slice.call(arguments, 2);
            var f = (function () {
                fun.apply(null, argu);
            });
            to = window.setTimeout(f, delay);
            tmpTimeOut.push(to);
            return to;
        }
        to = window.setTimeout(fun, delay);
        tmpTimeOut.push(to);
    }

    // 清空加载计时器
    function clearTmpTimeOut() {
        while (tmpTimeOut.length > 0) {
            var tto = tmpTimeOut.shift();
            window.clearTimeout(tto);
            tmpTimeOut.length = 0;
        }
    }

    function addMeta(attr) {
        var meta = document.createElement("meta"); for (var i = 0; i < attr.length; i++) {
            meta.setAttribute(attr[i][0], attr[i][1]);
        }
        document.getElementsByTagName("head")[0].appendChild(meta);
    }
}

