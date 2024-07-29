using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerManager : MonoBehaviour
{

    public MusicPlayer MusicPlayerUi;
    public PlaylistSO CurrentPlaylist;

    // Start is called before the first frame update
    void Start()
    {
        LoadSong(CurrentPlaylist);
    }

    public void LoadSong(PlaylistSO song)
    {
        if (song != null)
        {
            MusicPlayerUi.UpdateMusicPlayerUI(song);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
