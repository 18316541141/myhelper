$.validator.addMethod( "mobile", function( value, element ) {
	return this.optional( element ) || /^1[34578]\d{9}$/.test( value );
}, "手机号码格式错误" );

$.validator.addMethod( "phone", function( value, element ) {
	return this.optional( element ) || /^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$/.test( value );
}, "固话号码格式错误" );
$.validator.addMethod('charAndNum',function(value,element){
	return this.optional(element) || /^[a-z\d]+$/.test(value);
},'');
$.validator.addMethod('regexp',function(value,element,param){
	param.lastIndex = 0;
	return this.optional(element) || param.test(value);
},'');
$.validator.addMethod('passwordComplex',function(value,element){
	var charss=[['1','2','3','4','5','6','7','8','9','0'],
	['a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'],
	['~','!','@','#','$','%','^','&','*','(',')','_','-','+','=','\\','/','|','{','}','[',']','"','\'',':',';','<','>',',','.']];
	var contains=false;
	for(var i=0,len_i=charss.length;i<len_i;i++){
		contains=false;
		for(var j=0,len_j=charss[i].length;j<len_j;j++){
			if(value.indexOf(charss[i][j])>-1){
				contains=true;
				break;
			}
		}
		if(contains==false) break;
	}
	return this.optional(element) || contains;
},'');
//对js添加限制，确保不会出现xss攻击
$.validator.addMethod('jsLimit',function(value,element){
	var notContains=['<','>','eval(','location.','/','='];
	var ret=true;
	for(var i=0,len=notContains.length;i<len;i++)
		if(value.indexOf(notContains[i])>-1){
			ret=false;
			break;
		}
	return this.optional(element) || ret;
},'');