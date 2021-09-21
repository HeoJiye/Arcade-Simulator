using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Tetris : MonoBehaviour
{
	public Line[] line;
	public NextBlock[] nextBlock;
	public ScoreDigit[] scoreDigit;
	public Gauge gauge;

	public int[ , , ] random = new int[5, 4, 4];
	public int[ ] randomN = new int[5];

	public AudioSource[] sound;

	int[ , ] block_0;
	int[ , ] block_1;
	int[ , ] block_2;
	int[ , ] block_3;
	int[ , ] block_4;
	int[ , ] block_5;
	int[ , ] block_6;

	bool isMoving;

	int spawnX = 18;
	int spawnY = 5;

	int X;
	int Y;

	int frame = 0;

	int score = 0;

	// SETTING
		// 블럭 종류 설정
		void setting1() 
		{
			block_0 = new int[ , ] { {0, 0, 1, 0}, {0, 0, 1, 0}, {0, 0, 1, 0}, {0, 0, 1, 0} };
			block_1 = new int[ , ] { {0, 0, 0, 0}, {0, 1, 1, 0}, {0, 1, 1, 0}, {0, 0, 0, 0} };
			block_2 = new int[ , ] { {0, 0, 0, 0}, {0, 1, 1, 0}, {0, 0, 1, 1}, {0, 0, 0, 0} };
			block_3 = new int[ , ] { {0, 0, 0, 0}, {0, 0, 1, 1}, {0, 1, 1, 0}, {0, 0, 0, 0} };
			block_4 = new int[ , ] { {0, 0, 0, 0}, {0, 0, 1, 0}, {0, 0, 1, 0}, {0, 1, 1, 0} };
			block_5 = new int[ , ] { {0, 0, 0, 0}, {0, 1, 0, 0}, {0, 1, 0, 0}, {0, 1, 1, 0} };
			block_6 = new int[ , ] { {0, 0, 0, 0}, {0, 0, 1, 0}, {0, 1, 1, 0}, {0, 0, 1, 0} };
		}

		// randomN, random 조정
		void setting2()
		{
			for(int i = 0; i < 4; i++)
				randomN[i] = randomN[i+1];
			randomN[4] = Random.Range(0, 7);

			for(int i = 0; i < 5; i++) {
				if(randomN[i] == 0)
		            setting3(block_0, i);
		        else if(randomN[i] == 1)
		            setting3(block_1, i);
		        else if(randomN[i] == 2)
		            setting3(block_2, i);
		        else if(randomN[i] == 3)
		            setting3(block_3, i);
		        else if(randomN[i] == 4)
		            setting3(block_4, i);
		        else if(randomN[i] == 5)
		            setting3(block_5, i);
		        else if(randomN[i] == 6)
		            setting3(block_6, i);
			}

			X = spawnX; Y = spawnY;	
			setting4();

			isMoving = true;
			frame = 1;
		}

		// random[,,]에 블럭 넣기
		void setting3(int[,] block, int N) 
		{
			for(int i = 0; i < 4; i++) for(int j  = 0; j < 4; j++) 
				random[N, i, j] = block[i, j];
		}

		// Spawn
		void setting4() 
		{
			try {
				for(int p = 0; p < 4; p++) for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++)
					nextBlock[p].line[i].box[j].N = 0;

				for(int p = 0; p < 4; p++) for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++)
					if(random[p+1,i,j] == 1)
						nextBlock[p].line[i].box[j].N = randomN[p+1] + 1;

				for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++) {
					if(random[0, i, j] == 1) {
						if(line[X-i].box[Y+j].N != 0) {
							sound[200].Play(); // 일부러 오류냄
						}
						line[X-i].box[Y+j].N = randomN[0] + 1;
					}
				}
			}
			catch(Exception ex) {
				Debug.Log("GemeOver");
				sound[4].Play();
				Invoke("GameOver", 1f);
				isMoving = true;
			}
		}

		// 파.괴.
		void setting5()
		{
			if(!isMoving) {
				int N = 0;
				for(int i = 0; i < 19; i++) {
					bool isFull = true;
					for(int j = 0; j < 13; j++)	{
						if(line[i].box[j].N == 0) {
							isFull = false;
							break;
						}
					}
					if(isFull) {
						N++;
						for(int p = i; p < 18; p++) {
							for(int j = 0; j < 13; j++) {
								line[p].box[j].N = line[p+1].box[j].N;
							}
						}
						i--;
						setting6(Random.Range(N*100, N*N*200));
					}
				}
			}
		}

		// Score
		void setting6(int add)
		{
			sound[2].Play();
			score += add;
			int s = score;
			int i = 0;
			while(s > 0) {
				if(i > 8) {
					for(int j = 0; j < 9; j++)
						scoreDigit[i].set(9);
					break;
				}
				scoreDigit[i++].set(s % 10);
				s /= 10;
			}
		}

		void setting7() 
		{
			if(gauge.isFull() && !isMoving) 
			{
				sound[1].Play();

				for(int i = 0; i < 18; i++) for(int j = 0; j < 13; j++)
					line[18-i].box[j].N = line[18-(i+1)].box[j].N;

				bool isGameOver = false;
				for(int j = 0; j < 13; j++)
					if(line[18].box[j].N != 0) isGameOver = true;

				int empty = Random.Range(0, 13);
				for(int j = 0; j < 13; j++) {
					if(j != empty) line[0].box[j].N = 9;
					else line[0].box[j].N = 0;
				}

				if(isGameOver) {
					Debug.Log("GemeOver");
					sound[4].Play();
					Invoke("GameOver", 1f);
				}

				gauge.reset();
			}
		}

		void GameOver() {
			if(NetworkManager.getScore("Tetris") <= score) 
            	NetworkManager.UpdateScore("Tetris", score);
			SceneManager.LoadScene("TETRIS");
		}
	// SETTING


	void Start() 
	{
		setting1();

		for(int i = 0; i < 5; i++)
			randomN[i] = Random.Range(0, 7);
		setting2();
	}

	void Update() 
	{
		frame++;

		if(frame % 50 == 0) fall();
		if(frame % 60 == 0) gauge.up();

		if(frame == 10) setting5();
		if(frame == 20) setting7();
		if(frame == 30) {
			if(!isMoving) {
				setting2();
			}
		}
	}

	void FixedUpdate()
	{
		if(Input.GetKeyDown(KeyCode.A)) moveLeft();
		if(Input.GetKeyDown(KeyCode.D)) moveRight();
		if(Input.GetKeyDown(KeyCode.S)) moveDown();
		if(Input.GetKeyDown(KeyCode.W)) moveUp();
		if(Input.GetKeyDown(KeyCode.E)) moveRotate();
	}

	// Block Move
		public void moveLeft()
		{
			sound[3].Play();
			if(isMoving) {
				for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++) {
					if(random[0, i, j] == 1)
						line[X-i].box[Y+j].N = 0;
				}

				bool canMoving = true;
				for(int i = 0; i < 4; i++) {
					for(int j = 0; j < 4; j++) {
						if(random[0, i, j] == 1) { 
							if(Y+(j-1) < 0) {
								canMoving = false;
								break;
							}
							if (line[X-i].box[Y+(j-1)].N != 0) {
								canMoving = false;
								break;
							}
						}
					}
					if(!canMoving) break;
				}

				if(canMoving) Y--;

				for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++) {
					if(random[0, i, j] == 1)
						line[X-i].box[Y+j].N = randomN[0] + 1;
				}
			}
		}

		public void moveRight()
		{
			sound[3].Play();
			if(isMoving) {
				for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++) {
					if(random[0, i, j] == 1)
						line[X-i].box[Y+j].N = 0;
				}

				bool canMoving = true;
				for(int i = 0; i < 4; i++) {
					for(int j = 0; j < 4; j++) {
						if(random[0, i, j] == 1) { 
							if(Y+(j+1) > 12) {
								canMoving = false;
								break;
							}
							if (line[X-i].box[Y+(j+1)].N != 0) {
								canMoving = false;
								break;
							}
						}
					}
					if(!canMoving) break;
				}

				if(canMoving) Y++;

				for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++) {
					if(random[0, i, j] == 1)
						line[X-i].box[Y+j].N = randomN[0] + 1;
				}
			}
		}

		public void moveDown() {
			sound[3].Play();
			for(int i = 0; i < 5; i++)
				fall();
		}

		public void fall()
		{
			if(isMoving) {
				for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++) {
					if(random[0, i, j] == 1)
						line[X-i].box[Y+j].N = 0;
				}

				bool canMoving = true;
				for(int i = 0; i < 4; i++) {
					for(int j = 0; j < 4; j++) {
						if(random[0, i, j] == 1) { 
							if(X-(i+1) < 0) {
								canMoving = false;
								break;
							}
							if (line[X-(i+1)].box[Y+j].N != 0) {
								canMoving = false;
								break;
							}
						}
					}
					if(!canMoving) break;
				}

				if(canMoving) X--;
				else {
					isMoving = false;
					frame = 0;
				}

				for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++) {
					if(random[0, i, j] == 1)
						line[X-i].box[Y+j].N = randomN[0] + 1;
				}
			}
		}
		public void moveUp()
		{
			sound[3].Play();
			if(isMoving) {
				frame = 1;
			}
		}

		public void moveRotate()
		{
			sound[3].Play();
			if(isMoving) {
				for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++) {
					if(random[0, i, j] == 1)
						line[X-i].box[Y+j].N = 0;
				}

				bool canMoving = true;
				for(int i = 0; i < 4; i++) {
					for(int j = 0; j < 4; j++) {
						if(random[0, 4-j-1, i] == 1) {
							if (X-i > 18|| X-i < 0 || Y+j > 12 || Y+j < 0) {
								canMoving = false;
								break;
							}
							if (line[X-i].box[Y+j].N != 0 ) {
								canMoving = false;
								break;
							}
						}
					}
					if(!canMoving) break;
				}

				if(canMoving) {
					int[,] tmp = new int[4,4];
					for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++)
						tmp[i,j] = random[0, 4-j-1, i];
					for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++)
						random[0, i, j] = tmp[i,j];
				}

				for(int i = 0; i < 4; i++) for(int j = 0; j < 4; j++) {
					if(random[0, i, j] == 1)
						line[X-i].box[Y+j].N = randomN[0] + 1;
				}
			}
		}
}