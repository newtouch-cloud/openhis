//���ݵ���������������
$.CalcSfxmSl = function (zll, dwjls, jjcl, throwExWhenZero) {
    if (!!!zll) {
        throw new Error("ȱ��������");
        return;
    }
    if (!!!dwjls) {
        throw new Error("ȱ�ٵ�λ������");
        return;
    }
    if (!!!jjcl) {
        throw new Error("ȱ�ټƼ۲���");
        return;
    }
    var res = Math.floor(zll / dwjls);
    if (res <= 0 && !(throwExWhenZero === false)) {
        throw new Error("������0����");
        return;
    }
    return res;
}