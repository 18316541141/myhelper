'use strict';
var addEditLockLimitDateUrl='/';

/**
 * 删除下拉框指定值的选项
 * $select：下拉框
 * val：指定option的value值，不传入就删除当前选中值
 */
function removeOptionByVal($select,val){
	$select.find('option').each(function(){
		if(val==undefined && $(this).val()==$select.val() || $(this).val()==val){
			$(this).remove();
			return false;
		}
	});
}

/**
 * 获取下拉框选中项的文本描述
 * $select：jquery下拉框对象
 * return：返回下拉框选中项的文本描述
 */
function selectText($select){
	var selectedVal=$select.val();
	var retText;
	$select.find('option').each(function(){
		if($(this).val()==selectedVal){
			retText=$(this).text();
			return false;
		}
	});
	return retText;
}

/**
 * 设置下拉框的选中值
 * $select：jquery下拉框对象
 * val：设置的选中值，对应option的value
 */
function setSelectVal($select,val){
	$select.find('option').each(function(i){
		if($(this).val()==val){
			$select.get(0).selectedIndex=i;
			return false;
		}
	});
}

/**
 * 设置表格列的为顺序号
 * $tbody：设置表格的tbody
 * colIndex：列序号，从0开始
 * beginNum：起始数字
 * formatCallback；格式回调函数
 */
function setSortIndex($tbody,colIndex,beginNum,formatCallback){
	$tbody.find('tr').each(function(i){
		var $td=$(this).find('td:eq('+colIndex+')');
		if(formatCallback==undefined)
			$td.text(i+beginNum);
		else
			$td.text(formatCallback(i+beginNum));
	});
}

/**
 * 移动元素在文本流的位置
 * $ele：被移动的元素
 * moveStep：移动步伐，正数表示往后移动，
 *	负数表示往前移动。
 */
function moveElement($ele,moveStep){
	if(moveStep>0){
		var $nextTemp=$ele;
		for(var i=0;i<moveStep;i++)
			$nextTemp=$nextTemp.next();
		$nextTemp.after($ele);
	}else if(moveStep<0){
		var $prevTemp=$ele;
		for(var i=0;i>moveStep;i--)
			$prevTemp=$prevTemp.prev();
		$prevTemp.before($ele);
	}
}

/**
 * 生成一个整数输入框回调函数，该回调函数用于input事件回调，确保整数
 * 输入框无论如何输入的都是整数。（仅适用于jquery的绑定input事件功能）
 * min：输入整数的最小值限制
 * max：输入整数的最大值限制
 * return：数字输入框的回调函数
 */
function getIntInputCallback(min,max){
	return function(){
		var num = $(this).val();
		if (isNaN(parseInt(num)))
			$(this).val(min);
		else if (parseInt(num)<min)
			$(this).val(min);
		else if (parseInt(num)>max)
			$(this).val(max);
		else
			$(this).val(parseInt(num));
	}
}

/**
 * 对增加数字的按钮生成回调函数
 * $input：数字显示的文本框
 * min：数字的最小值
 * max：数字的最大值
 * return：该按钮的回调函数
 */
function getAddIntCallback($input,min,max){
	return function(){
		var num = $input.val();
		if (isNaN(parseInt(num)))
			$input.val(min);
		else if (parseInt(num) < min)
			$input.val(min);
		else if (parseInt(num) > max-1)
			$input.val(max);
		else
			$input.val(parseInt(num) + 1);
	};
}

/**
 * 对减少数字的按钮生成回调函数
 * $input：数字显示的文本框
 * min：数字的最小值
 * max：数字的最大值
 * return：该按钮的回调函数
 */
function getReduceIntCallback($input,min,max){
	return function(){
		var num = $input.val();
		if (isNaN(parseInt(num)))
			$input.val(min);
		else if (parseInt(num) < min+1)
			$input.val(min);
		else if (parseInt(num) > max)
			$input.val(max);
		else
			$input.val(parseInt(num) - 1);
	};
}

/**
 * 生成指定长度的随机字符串
 * len：随机字符串长度
 * type：生成规则，规则如下：
 *	001（只含数字）、010（只含小写字母）、100（只含大写字母）；或者左边情况的混合。
 * return：返回随机字符串
 */
function randomStr(len,type){
	var randomCharArray = [];
	if(type & 1==1)
		randomCharArray=randomCharArray.concat(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']);
	if((type >> 1) & 1==1)
		randomCharArray=randomCharArray.concat(['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z']);
	if((type >> 2) & 1==1)
		randomCharArray=randomCharArray.concat(['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z']);
	var str = '';
	for (var i = 0; i < len; i++)
		str += randomCharArray[parseInt(Math.random() * randomCharArray.length)];
	return str;
}

/**
 * 定期延长编辑的锁定时间
 * controllerName: 控制器名称
 * primaryKey: 数据主键 
 * username: 用户名
 * addSeconds: 延长的秒数
 */
function addEditLockLimitDate(controllerName,primaryKey,addSeconds){
	setInterval(function(){
		$.post(addEditLockLimitDateUrl,{controllerName:controllerName,primaryKey:primaryKey,addSeconds:addSeconds},function(){
		},'json');
	},addSeconds*1000);
}

/*
 * 实时刷新数据
 * url: 实时刷新数据来源的url
 * data: 请求参数，这些参数必须是回调函数，可以动态调用
 * callback: 响应请求结果的回调函数，如果回调函数返回false，则跳出实时刷新的循环。
 */
var realTimeRefresh = (function () {
    var versionMap = {};
    return function(url,data,callback){
        var postData = { version: versionMap[url]};
        if ($.type(data) == 'object') {
            for(var key in data){
                if(data.hasOwnProperty(key))
                    postData[key]=data[key]();
            }
        } else if ($.type(data)=='function') {
            callback = data;
        }
        $.post(url,postData,function(ret){
            var callRet = callback(ret);
            versionMap[url] = ret['version'];
            if (callRet == false) return;
            realTimeRefresh(url, data, callback);
        },'json');
    }
}());

/*
 * 获取滚动条宽度
 */
var getScrollWidth=(function() {  
	var noScroll, scroll, oDiv = document.createElement("DIV");  
	oDiv.style.cssText = "position:absolute; top:-1000px; width:100px; height:100px; overflow:hidden;";
	noScroll = document.body.appendChild(oDiv).clientWidth;
	oDiv.style.overflowY = "scroll";
	scroll = oDiv.clientWidth;
	document.body.removeChild(oDiv);
	var scrollWidth=noScroll-scroll;
	return function(){
		return scrollWidth;
	}
}());

/**
 * 使用get的方式打开一个新窗口
 * url：新窗口的url地址
 * params：请求参数
 */
function getOpenWin(url,params){
	var times=new Date().getTime();
	var input='';
	if(params){
		for(var key in params){
			if(params.hasOwnProperty(key))
				input+='<textarea name="'+key+'">'+params[key]+'</textarea>';
		}
	}
	$('body').append('<form style="display:none;" id="'+times+'" method="get" target="_blank" action="'+url+'">'+input+'</form>');
	if(params){
		for(var key in params){
			if(params.hasOwnProperty(key))
				$('#'+times).find('textarea[name="'+key+'"]').val(params[key]);
		}
	}
	$('#'+times).submit().remove();
}

/**
 * 提交post请求到一个隐藏的iframe
 * url：提交的url
 * params：提交的参数
 */
function postIframe(url,params){
	var times=new Date().getTime();
	var input='';
	if(params){
		for(var key in params){
			if(params.hasOwnProperty(key))
				input+='<textarea name="'+key+'"></textarea>';
		}
	}
	$('body').append('<form style="display:none;" id="'+times+'" target="'+times+'" method="post" action="'+url+'">'+input+'</form>');
	$('body').append('<iframe name="'+times+'" style="display:none;"></iframe>');
	if(params){
		for(var key in params){
			if(params.hasOwnProperty(key))
				$('#'+times).find('textarea[name="'+key+'"]').val(params[key]);
		}
	}
	$('#'+times).submit();
	$('iframe[name="'+times+'"]').load(function(){ 
        $('#'+times).remove();
		$('iframe[name="'+times+'"]').remove();
    });
}

/**
 * 提交get请求到一个隐藏的iframe
 * url：提交的url
 * params：提交的参数
 */
function getIframe(url,params){
	var times=new Date().getTime();
	var input='';
	if(params){
		for(var key in params){
			if(params.hasOwnProperty(key))
				input+='<textarea name="'+key+'"></textarea>';
		}
	}
	$('body').append('<form style="display:none;" id="'+times+'" target="'+times+'" method="get" action="'+url+'">'+input+'</form>');
	$('body').append('<iframe name="'+times+'" style="display:none;"></iframe>');
	if(params){
		for(var key in params){
			if(params.hasOwnProperty(key))
				$('#'+times).find('textarea[name="'+key+'"]').val(params[key]);
		}
	}
	$('#'+times).submit();
	$('iframe[name="'+times+'"]').load(function(){ 
        $('#'+times).remove();
		$('iframe[name="'+times+'"]').remove();
    });
}

/**
 * 在提交表单请求到当前窗口
 * url：新窗口的url地址
 * params：请求参数
 */
function postCurrentWin(url,params){
	var times=new Date().getTime();
	var input='';
	if(params){
		for(var key in params){
			if(params.hasOwnProperty(key))
				input+='<textarea name="'+key+'"></textarea>';
		}
	}
	$('body').append('<form style="display:none;" id="'+times+'" method="post" action="'+url+'">'+input+'</form>');
	if(params){
		for(var key in params){
			if(params.hasOwnProperty(key))
				$('#'+times).find('textarea[name="'+key+'"]').val(params[key]);
		}
	}
	$('#'+times).submit();
	$('#'+times).remove();
}

/**
 * 使用post的方式打开一个新窗口
 * @url：新窗口的url地址
 * @params：post请求参数
 * @searchParam：url上的参数
 */
function postOpenWin(url, params, searchParam) {
    var times = new Date().getTime();
    var input = '';
    if (params) {
        for (var key in params) {
            if (params.hasOwnProperty(key))
                input += '<textarea name="' + key + '"></textarea>';
        }
    }
    if ($.type(searchParam) === 'object') {
        for (var key in searchParam) {
            if (searchParam.hasOwnProperty(key))
                input += '<textarea name="' + key + '"></textarea>';
        }
    }
    $('body').append('<form style="display:none;" id="' + times + '" method="post" target="_blank" action="' + url + '">' + input + '</form>');
    if (params) {
        for (var key in params) {
            if (params.hasOwnProperty(key))
                $('#' + times).find('textarea[name="' + key + '"]').val(params[key]);
        }
    }
    if ($.type(searchParam) === 'object') {
        for (var key in searchParam) {
            if (searchParam.hasOwnProperty(key))
                $('#' + times).find('textarea[name="' + key + '"]').val(searchParam[key]);
        }
    }
    $('#' + times).submit();
    $('#' + times).remove();
}

/**
 * 获取url上?后面的参数值
 * pName：参数名
 */
function urlParam(pName){
	this.jqTest();
	if(!pCache.hasOwnProperty(pName)){
		var kvArrays=window.location.search.substring(1).split('&');
		var kvArray;
		for(var i=0,len=kvArrays.length;i<len;i++){
			kvArray=kvArrays[i].split('=');
			pCache[kvArray[0]]=kvArray.length===1?'':decodeURI(kvArray[1]);
		}
	}
	return pCache[pName];
}


/**
 * 获取密码强度评分
 * password：密码
 * return：返回强度评分
 */
function getPasswordScore(password) {
	password = password.replace(/\s/, ''); //去空格
	if (password.length == 0) return 0;
	var lowerPasswordString = password.toLowerCase();//全部转为小写形式
	var score = 0;//得分分数
	var nLen = password.length;//密码字符总个数
	var nAlphaUC = 0; //大写英文字母个数
	var nAlphaLC = 0;  //小写英文字母个数
	var nNumber = 0;  //数字字符个数
	var nSymbol = 0;  //特殊字符个数
	var nMidChar = 0; //密码中间部分出现数字或者特殊字符的个数
	var nRequirements = 0;//达到最低需求四个条件的个数
	var nRepChar = 0; //重复字符的个数
	var nConsecAlphaUC = 0; //连续大写字母个数
	var nConsecAlphaLC = 0; //连续小写字母个数
	var nConsecNumber = 0;  //连续数字字符个数
	var nSeqAlpha = 0;//依顺序出现的字母序列的个数
	var nSeqNumber = 0;//依顺序出现的数字序列的个数
	var nTmpAlphaUC = 0;
	var nTmpAlphaLC = 0;
	var nTmpNumber = 0;

	for (var i = 0; i < nLen; i++) {
		if (password[i]==password[i].toUpperCase() && password[i].toLowerCase()!=password[i].toUpperCase()) {
			if (nTmpAlphaUC != 0){
				if ((nTmpAlphaUC + 1) == i) 
					nConsecAlphaUC++;
			}
			nTmpAlphaUC = i;
			nAlphaUC++;
		}
		else if (password[i]==password[i].toLowerCase() && password[i].toLowerCase()!=password[i].toUpperCase()) {
			if (nTmpAlphaLC != 0) {
				if ((nTmpAlphaLC + 1) == i) 
					nConsecAlphaLC++;
			}
			nTmpAlphaLC = i;
			nAlphaLC++;
		}
		else if (!isNaN(password[i])) {
			if (i > 0 && i < (nLen - 1))
				nMidChar++;
			if (nTmpNumber != 0) {
				if ((nTmpNumber + 1) == i) 
					nConsecNumber++;
			}
			nTmpNumber = i;
			nNumber++;
		}
		else {
			if (i > 0 && i < (nLen - 1))
				nMidChar++;
			nSymbol++;
		}
		for (var j = 0; j < nLen; j++) {
			if (i != j && lowerPasswordString[i]==lowerPasswordString[j])
				nRepChar++;
		}
	}
	var sAlphas = "abcdefghijklmnopqrstuvwxyz";
	var sNumerics = "01234567890";
	var nSeqCount = 3;//规则:依顺序出现三个以上
	for (var i = 0; i < sAlphas.length - nSeqCount; i++) {
		var sFwd = sAlphas.substring(i, i + nSeqCount);
		var sRev = sFwd.split('').reverse();
		if (lowerPasswordString.indexOf(sFwd) != -1 || lowerPasswordString.indexOf(sRev.join('')) != -1) nSeqAlpha++;
	}
	for (var i = 0; i < sNumerics.length - nSeqCount; i++) {
		var sFwd = sNumerics.substring(i, i+nSeqCount);
		var sRev = sFwd.split('').reverse();
		if (lowerPasswordString.indexOf(sFwd) != -1 || lowerPasswordString.indexOf(sRev.join('')) != -1) nSeqNumber++;
	}
	score += nLen * 4;     //密码长度，加分
	score += nAlphaUC * 2;  // 大写字母字符个数，加分
	score += nAlphaLC * 2;  // 小写字母字符个数，加分
	score += nNumber * 4;   // 数字字符个数，加分
	score += nSymbol * 6;   // 特殊字符个数，加分
	score += nMidChar * 2;  // 密码中间部分出现数字或者特殊字符, 加分
	if (nAlphaUC > 0) nRequirements++;
	if (nAlphaLC > 0) nRequirements++;
	if (nNumber > 0) nRequirements++;
	if (nSymbol > 0) nRequirements++;
	if (nLen >= 8 && nRequirements >= 3) score += (nRequirements + 1) * 2; //满足最低需求, 加分
	if ((nAlphaLC > 0 || nAlphaUC > 0) && nSymbol == 0 && nNumber == 0) score -= nLen; //只有英文字母字符,扣分
	if (nAlphaLC == 0 && nAlphaUC == 0 && nSymbol == 0 && nNumber > 0) score -= nLen; //只有数字字符,扣分
	score -= nRepChar; //出现重复字符(忽略大小写), 扣分
	score -= nConsecAlphaUC * 2;   //大写字母连续（例如AD）,扣分
	score -= nConsecAlphaLC * 2;   //小写字母连续（例如AD）,扣分
	score -= nConsecNumber * 2;    //数字字符连续（例如13）,扣分
	score -= nSeqAlpha * 3;      //字母依顺序出现 (三个以上，例如abc）,扣分
	score -= nSeqNumber * 3;      //数字依顺序出现 (三个以上，例如123）,扣分
	if (score < 0) score = 0; 
	if (score > 100) score = 100;
	return score;
}

/**
 * 密码强度描述
 * paswword：传入一个密码
 * return：返回密码强度描述
 */
function getPasswordLevel(paswword){
	var nScore = getPasswordScore(paswword);
	if (nScore >= 0 && nScore < 20) return '安全性非常低';
	if (nScore >= 20 && nScore < 40) return '安全性较低';
	if (nScore >= 40 && nScore < 60) return '安全性一般';
	if (nScore >= 60 && nScore < 80) return '安全性较好';
	if (nScore >= 80) return '安全性非常好';
	return '这是空密码';
}