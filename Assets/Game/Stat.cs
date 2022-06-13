using System;

public static class Stat
{
    public static int playerCount = 2;
    public static string[] namePlayers = new string[] {"����� 1", "����� 2", "����� 3", "����� 4" };
    public static int[] pointsPlayers = new int[] {0, 0, 0, 0};
    public static string[] ingerd = new string[18] { "����", "������", "�������", "�����", "������", "�����",
        "���", "����", "�������", "������", "����", "����", "������", "�������", "���", "������", "�����", "�������" };

    public static string[] rands = new string[5] { "�� �������� 50 ������!", "������������� �� ������ ������", "��� ���� ����� � ������ ������",
        "����������� ������� �� ��������� �������", "����������� ������������ ������!" };

    public static int roll;
    public static int winner = 0;
    public static int maxPoints = 1000;

    public static T[] RemoveAt<T>(this T[] source, int index)
    {
        T[] dest = new T[source.Length - 1];
        if (index > 0)
            Array.Copy(source, 0, dest, 0, index);

        if (index < source.Length - 1)
            Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

        return dest;
    }
}
