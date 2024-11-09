CREATE TRIGGER TrackDDLChanges
ON DATABASE
FOR CREATE_PROCEDURE, ALTER_PROCEDURE, DROP_PROCEDURE,
    CREATE_FUNCTION, ALTER_FUNCTION, DROP_FUNCTION,
    CREATE_TABLE, ALTER_TABLE, DROP_TABLE
AS
BEGIN
    DECLARE @EventData XML = EVENTDATA();

    INSERT INTO DDLChangeLog (
        EventType,
        ObjectName,
        ObjectType,
        TSQLCommand,
        LoginName,
        ChangeDate
    )
    VALUES (
        @EventData.value('(/EVENT_INSTANCE/EventType)[1]', 'NVARCHAR(100)'),
        @EventData.value('(/EVENT_INSTANCE/ObjectName)[1]', 'NVARCHAR(256)'),
        @EventData.value('(/EVENT_INSTANCE/ObjectType)[1]', 'NVARCHAR(100)'),
        @EventData.query('(/EVENT_INSTANCE/TSQLCommand)[1]').value('.', 'NVARCHAR(MAX)'), -- 保留原始换行符
        @EventData.value('(/EVENT_INSTANCE/LoginName)[1]', 'NVARCHAR(256)'),
        GETDATE()
    );
END;
