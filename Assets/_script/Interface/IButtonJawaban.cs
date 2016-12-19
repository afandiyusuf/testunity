using UnityEngine;
using System.Collections.Generic;
//! interface button jawaban
public interface IButtonJawaban {
    void initThisButton(IMiniGameSoal SoalReference,bool isTrueAnswer,string label);
    void SendJawaban();

}
