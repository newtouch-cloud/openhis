//根据单次治疗量算数量
$.CalcSfxmSl = function (zll, dwjls, jjcl, throwExWhenZero) {
    if (!!!zll) {
        throw new Error("缺少治疗量");
        return;
    }
    if (!!!dwjls) {
        throw new Error("缺少单位计量数");
        return;
    }
    if (!!!jjcl) {
        throw new Error("缺少计价策略");
        return;
    }
    var res = Math.floor(zll / dwjls);
    if (res <= 0 && !(throwExWhenZero === false)) {
        throw new Error("不允许0数量");
        return;
    }
    return res;
}