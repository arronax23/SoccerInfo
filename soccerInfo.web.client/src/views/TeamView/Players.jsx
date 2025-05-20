import { useEffect, useState, useRef } from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/css";
import Player from "./Player";
import SlideButton from "./Helpers/SlideButton"

const Players = ({ teamId }) => {
  const [players, setPlayers] = useState();
  useEffect(() => {
    getPlayers();
  }, []);

  async function getPlayers() {
    const response = await fetch(`/api/GetPlayers/${teamId}`);
    const data = await response.json();
    console.log(data);
    setPlayers(data);
  }

  const swiperRef = useRef(null);

  return (
    <div className="players">
      <SlideButton swiperRef={swiperRef} isNext={true} />
      <SlideButton swiperRef={swiperRef} isNext={false} />

      <Swiper
        slidesPerView={6}
        onSlideChange={() => console.log("slide change")}
        onSwiper={(swiper) => (swiperRef.current = swiper)}
        style={{width: "90%"}}
      >
        {players &&
          players.map((p) => (
            <SwiperSlide key={p.id} style={{ alignSelf: "center" }} >
              <Player
                key={p.id}
                id={p.id}
                name={p.name}
                position={p.position}
                faceImageBase64={p.faceImageBase64}
                age={p.age}
                dateOfBirth={p.dateOfBirth}
                marketValue={p.marketValue}
                marketValueUnit={p.marketValueUnit}
                nationalityImages={p.nationalityImageBase64Collection}
              />
            </SwiperSlide>
          ))}
      </Swiper>
    </div>
  );
};

export default Players;
