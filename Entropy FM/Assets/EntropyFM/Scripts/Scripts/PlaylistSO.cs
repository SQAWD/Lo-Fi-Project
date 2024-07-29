using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlaylistMenu", menuName = "New Playlist")]
public class PlaylistSO : ScriptableObject
{
    
    public Sprite AlbumCover;
    public string Title;
    public string Artist;
    public AudioClip AudioFile;
    public string AlbumLink;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
