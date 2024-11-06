# SQL Server ʵ������
$ServerName = "."  # �滻Ϊ�� SQL Server ʵ������
$BackupFolder = "C:\Backup\BackUpJobs"  # �滻Ϊ�����ļ�����Ŀ¼

# ���� SQL Server SMO ������
[System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.Smo') | Out-Null

# ���� SQL Server ����
$Server = New-Object Microsoft.SqlServer.Management.Smo.Server $ServerName

# ��ȡ������ҵ�����б�
$ExistingJobNames = $Server.JobServer.Jobs | ForEach-Object { $_.Name }

# ��������Ŀ¼�е����� .sql �ļ�
Get-ChildItem -Path $BackupFolder -Filter *.sql | ForEach-Object {
    $FilePath = $_.FullName
    $JobName = $_.BaseName  # ���ļ����л�ȡ��ҵ����
    Write-Host "���ڻָ���ҵ�ű�: $FilePath"
    Write-Host "��ȡ�� JobName ��: '$JobName'"  # �����ȡ����ҵ����

    # �����ҵ�Ƿ��Ѵ���
    if ($ExistingJobNames -contains $JobName) {
        Write-Host "��ҵ '$JobName' �Ѵ��ڣ������ָ���"
        return  # �����ָ�����ҵ
    }

    # ��ȡ�ļ�����
    $Script = Get-Content -Path $FilePath -Raw

    # ִ�нű�����
    try {
        $Server.ConnectionContext.ExecuteNonQuery($Script)
        Write-Host "�ɹ��ָ���ҵ '$JobName'"
    } catch {
        Write-Host "�ָ���ҵ '$JobName' ʱ����: $_"
    }
}

Write-Host "������ҵ�ѳɹ��ָ��� $ServerName"
