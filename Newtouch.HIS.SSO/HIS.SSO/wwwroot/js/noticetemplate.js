function noticeMrqc(objStr) {
    var jsonObj = JSON.parse(objStr);
    return '<div class="list-group"><div class="list-group-item"><span class="list-group-item-heading">'
        + jsonObj.xm + '（ ' + jsonObj.jzh + '--' + jsonObj.ch + '--' + jsonObj.ks + ' ）' + '</span></div><div class="list-group-item"><span class="list-group-item-heading">病历：'
        + jsonObj.hzwd + '</span><p class="list-group-item-text">质控描述：' + jsonObj.fknr + '</p><p class="list-group-item-text">限期处理：' + $.getTime({ date: jsonObj.qxclsj }); + '</p></div></div>';
    //    return '<ul class="list-group"><li class="list-group-item">' + jsonObj.xm + '(' + jsonObj.jzh + '--' + jsonObj.ch + ')'
    //        + '</li><li class="list-group-item">科室：' + jsonObj.ks + '</li><li class="list-group-item">病历：' + jsonObj.hzwd
    //        + '</li><li class="list-group-item">质控描述：' + jsonObj.fknr + '</li><li class="list-group-item">限期处理：' + jsonObj.qxclsj + '</li></ul>';
}