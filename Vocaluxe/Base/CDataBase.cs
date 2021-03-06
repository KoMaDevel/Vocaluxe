﻿#region license
// This file is part of Vocaluxe.
// 
// Vocaluxe is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Vocaluxe is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Vocaluxe. If not, see <http://www.gnu.org/licenses/>.
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using Vocaluxe.Lib.Database;
using VocaluxeLib;
using VocaluxeLib.Draw;

namespace Vocaluxe.Base
{
    static class CDataBase
    {
        private static CHighscoreDB _HighscoreDB;
        private static CCoverDB _CoverDB;
        private static CResourceDB _ResourceDB;

        public static bool Init()
        {
            _HighscoreDB = new CHighscoreDB(CConfig.FileHighscoreDB);
            _CoverDB = new CCoverDB(Path.Combine(CSettings.DataFolder, CSettings.FileNameCoverDB));
            _ResourceDB = new CResourceDB(Path.Combine(CSettings.ProgramFolder, CSettings.FileNameCreditsRessourcesDB));

            if (!_HighscoreDB.Init())
            {
                CLog.LogError("Error initializing Highscore-DB", true, true);
                return false;
            }
            if (!_CoverDB.Init())
            {
                CLog.LogError("Error initializing Cover-DB", true, true);
                return false;
            }
            if (!_ResourceDB.Init())
            {
                CLog.LogError("Error initializing Credits-DB", true, true);
                return false;
            }
            return true;
        }

        public static void Close()
        {
            if (_HighscoreDB != null)
            {
                _HighscoreDB.Close();
                _HighscoreDB = null;
            }
            if (_CoverDB != null)
            {
                _CoverDB.Close();
                _CoverDB = null;
            }
            if (_ResourceDB != null)
            {
                _ResourceDB.Close();
                _ResourceDB = null;
            }
        }

        public static bool GetDataBaseSongInfos(string artist, string title, out int numPlayed, out DateTime dateAdded, out int highscoreID)
        {
            return _HighscoreDB.GetDataBaseSongInfos(artist, title, out numPlayed, out dateAdded, out highscoreID);
        }

        public static List<SDBScoreEntry> LoadScore(int songID, EGameMode gameMode)
        {
            return _HighscoreDB.LoadScore(songID, gameMode);
        }

        public static int AddScore(SPlayer player)
        {
            return _HighscoreDB.AddScore(player);
        }

        public static void IncreaseSongCounter(int dataBaseSongID)
        {
            _HighscoreDB.IncreaseSongCounter(dataBaseSongID);
        }

        public static bool GetCover(string fileName, ref CTextureRef tex, int maxSize)
        {
            return _CoverDB.GetCover(fileName, ref tex, maxSize);
        }

        public static void CommitCovers()
        {
            _CoverDB.CommitCovers();
        }

        public static bool GetCreditsRessource(string fileName, ref CTextureRef tex)
        {
            return _ResourceDB.GetCreditsRessource(fileName, ref tex);
        }
    }
}