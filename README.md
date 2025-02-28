# :penguin: Penguin - Run 
<a href="https://git.io/typing-svg"><img src="https://readme-typing-svg.demolab.com?font=Fira+Code&size=72&duration=2000&pause=5000&color=F7F7F7&background=000000&center=true&vCenter=true&width=600&height=100&lines=PenguinRun" alt="Typing SVG" /></a>




<img src = "https://github.com/user-attachments/assets/825ac18e-56d4-4434-8ffc-a148ce9517de">


펭귄런은 장애물을 점프와 슬라이딩으로 피하면서
눈송이를 먹으면 점수가 오르는 러닝 게임입니다. 


## :book: 구현
#### 주정민:
- 2D 캐릭터 컨트롤러를 담당하여 이동, 체력 및 피격 시스템, 시간에 따른 중력 변화 등을 구현하였습니다. 
- 또한, 애니메이션, 입력, 물리, UI를 유기적으로 연결하여 플레이어 캐릭터의 핵심 동작을 제어하는 스크립트를 작성하였습니다.


#### 최형빈 : 아이템 배치 및 기능 구현
- 아이템 배치 : 아이템 스포너를 프리펩으로 만들어 타일맵의 자식 오브젝트로 설정하여 게임이 시작되었을 때와 타일맵이 루프할 때 일정한 간격으로 아이템을 배치하는 기능을 구현했습니다.
- 각 아이템 클래스는 BaseItem 클래스를 상속받으며 캐릭터 콜라이더가 아이템 오브젝트의 콜라이더와 충돌했을 때 아이템의 Use 메서드를 호출하도록 만들었습니다.


#### 김여진: 
- 스타트씬을 제작하였습니다.
- 게임중 현재점수, 최고점수를 반영, 저장하는 기능을 구현하였습니다.
- 게임중 결과를 결과창에 반영되는 UI를 제작하였습니다.


#### 변상윤: UIManager.cs, AudioManager.cs <Sound.cs>, CharacterColorLoad.cs 구현했습니다.
- UIManager의 역할은 언제 어디서나 접근할 수 있는 싱글톤이기에 게임오브젝트를 저장하기에는 어울리지 않아서 UI 게임오브젝트를 따로 단일 클래스에 저장하면서,
  단일 클래스에 기능을 구현한다음 UIManager에 구현한 Action(대리호출자)에 집어넣고, 스코어 아이템을 먹을때 호출하도록 설정했습니다.
- AudioManager는 백그라운드 뮤직, 효과음 조절을 할 수 있는 매니저이며, 씬이 변경될때도 내가 설정한 소리크기가 저장이 됩니다.
  당연히 게임을 종료해도 저장되도록 만들었으며, 효과음이 출력이될때 백그라운드뮤직 사운드를 낮춰지고, 점점 원래 소리크기로 되돌아 옵니다.
- CharacterColorLoad 클래스는 커스텀씬에서 플레이어가 설정했던 컬러를 저장해주고 있으며 게임을 종료후 다시 실행해도 기존에 선택된 컬러로 처음에 보여지게 됩니다.


#### 송민경 : TutorialManager.cs , Level.cs 구현하였습니다.
- Tutorail Manager코루틴 함수를 이용해 점프, 슬라이드의 조작법을 익히고 시간을 잠깐 멈추어 조작법의 안내를 볼 수 있습니다.
- 펭귄이 죽기전 까지 맵이 반복되어야하기 때문에 펭귄의 -x방향으로 콜라이더를 달아놓고
  카메라를 지나간 타일맵은 마지막 타일맵의 끝에 두어서 반복되게 구현하였습니다.



## :video_game: Link
[Penguin Run](https://byeonsangyoon.itch.io/penguinrun)

