using System;
using System.Collections;

var lines = File.ReadLines("./data.txt").ToList();
var pairs = lines.Chunk(3).Select(l =>
{
    var line1 = l[0];
    var line2 = l[1];
    return (GetValueLine(line1), GetValueLine(line2));
}).ToList();

Console.WriteLine();

List<object> GetValueLine(string line)
{
    // return new List<object>();
    return GetList(line, 1).result;
}

(List<object> result, int newIndex) GetList(string line, int index)
{
    var result = new List<object>();
    for (; index < line.Length - 1; index++)
    {
        if (line[index] == '[')
        {
            var res = GetList(line, index + 1);
            result.Add(res.result);
            index = res.newIndex;
        }
        else if (char.IsNumber(line[index])) result.Add(line[index] - '0');
        else if (line[index] == ']')
        {
            break;
        }
    }
    return (result, index);
};
var result = 0;
for(var i = 0; i < pairs.Count; i++)
{
    if (IsCorrect(pairs[i].Item1, pairs[i].Item2) == true) result += (i + 1);
}

Console.WriteLine(result);


bool? IsCorrect(object o1, object o2)
{
    if (o1 is int && o2 is int) {
        if ((int)o1 == (int)o2) return null;
        return (int)o2 > (int)o1;
    }

    if(o1 is int && IsList(o2))
    {
        return IsCorrect(new List<object> { (int)o1 }, o2);
    }


    if (o2 is int && IsList(o1))
    {
        return IsCorrect(o1, new List<object> { (int)o2 });
    }

    var l1 = o1 as List<object>;
    var l2 = o2 as List<object>;

    if (!l1.Any() && l2.Any()) return true;
    if (l1.Any() && !l2.Any()) return false;

    for(var i = 0; i < l1.Count; i ++)
    {

        if (i == l2.Count) return false;
        var res = IsCorrect(l1[i], l2[i]);
        if (res != null) return res;
        if (i + 1 == l1.Count && l1.Count < l2.Count) return true;
    }

    return null;
}

bool IsList(object o)
{
    return o is IList &&
       o.GetType().IsGenericType &&
       o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
}