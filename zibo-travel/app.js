(function () {
  const itinerary = window.ZIBO_ITINERARY;
  const locations = itinerary.locations;
  const legs = itinerary.legs;
  const svgWidth = 1000;
  const svgHeight = 1200;
  const autoplayMs = 3300;

  const els = {
    markerLayer: document.getElementById("markerLayer"),
    routeShadow: document.getElementById("routeShadow"),
    routeTrack: document.getElementById("routeTrack"),
    routeLive: document.getElementById("routeLive"),
    legBadge: document.getElementById("legBadge"),
    progress: document.getElementById("panelProgress"),
    day: document.getElementById("stationDay"),
    slot: document.getElementById("stationSlot"),
    name: document.getElementById("stationName"),
    note: document.getElementById("stationNote"),
    dishRail: document.getElementById("dishRail"),
    driveLabel: document.getElementById("driveLabel"),
    driveCaption: document.getElementById("driveCaption"),
    navLink: document.getElementById("navLink"),
    prev: document.getElementById("prevButton"),
    play: document.getElementById("playButton"),
    next: document.getElementById("nextButton"),
    timeline: document.getElementById("timeline"),
    optionalBlock: document.getElementById("optionalBlock"),
  };

  let activeIndex = 0;
  let timer = null;
  let playing = true;
  let routeLength = 1;
  let markers = [];

  function toSvgPoint(location) {
    return {
      x: (location.x / 100) * svgWidth,
      y: (location.y / 100) * svgHeight,
    };
  }

  function buildRoutePath() {
    const points = locations.map(toSvgPoint);
    const commands = [`M ${points[0].x} ${points[0].y}`];

    for (let i = 1; i < points.length; i += 1) {
      const prev = points[i - 1];
      const current = points[i];
      const direction = i % 2 === 0 ? 1 : -1;
      const distanceX = current.x - prev.x;
      const distanceY = current.y - prev.y;
      const bend = Math.min(125, Math.max(46, Math.abs(distanceY) * 0.22 + Math.abs(distanceX) * 0.1));
      const c1 = {
        x: prev.x + distanceX * 0.28 + direction * bend,
        y: prev.y + distanceY * 0.16,
      };
      const c2 = {
        x: prev.x + distanceX * 0.72 - direction * bend,
        y: prev.y + distanceY * 0.84,
      };
      commands.push(`C ${c1.x} ${c1.y}, ${c2.x} ${c2.y}, ${current.x} ${current.y}`);
    }

    const d = commands.join(" ");
    [els.routeShadow, els.routeTrack, els.routeLive].forEach((path) => {
      path.setAttribute("d", d);
    });

    requestAnimationFrame(() => {
      routeLength = els.routeLive.getTotalLength();
      els.routeLive.style.strokeDasharray = routeLength;
      renderActive();
    });
  }

  function amapSearchUrl(keyword) {
    const query = encodeURIComponent(keyword);
    const city = encodeURIComponent(itinerary.meta.city);
    return `https://uri.amap.com/search?keyword=${query}&city=${city}&view=map&src=zibo_fire_route`;
  }

  function getInboundLeg(index) {
    if (index === 0) {
      return null;
    }

    return legs[index - 1];
  }

  function renderMarkers() {
    els.markerLayer.innerHTML = "";
    markers = locations.map((location, index) => {
      const button = document.createElement("button");
      button.className = "marker";
      button.type = "button";
      button.style.left = `${location.x}%`;
      button.style.top = `${location.y}%`;
      button.setAttribute("aria-label", `查看第 ${index + 1} 站：${location.name}`);
      button.innerHTML = `
        <span class="marker-dot">${index + 1}</span>
        <span class="marker-label">${location.shortName}</span>
      `;
      button.addEventListener("click", () => {
        setActive(index);
        pauseAutoplay();
      });
      els.markerLayer.appendChild(button);
      return button;
    });
  }

  function renderDishes(location) {
    els.dishRail.innerHTML = "";
    const dishes = location.dishes.length ? location.dishes : [location.kind, location.area];
    dishes.forEach((dish) => {
      const chip = document.createElement("span");
      chip.className = "dish-chip";
      chip.textContent = dish;
      els.dishRail.appendChild(chip);
    });
  }

  function renderActive() {
    const location = locations[activeIndex];
    const leg = getInboundLeg(activeIndex);
    const progress = activeIndex / (locations.length - 1);

    markers.forEach((marker, index) => {
      marker.classList.toggle("is-active", index === activeIndex);
    });

    els.progress.style.width = `${Math.max(3, progress * 100)}%`;
    els.routeLive.style.strokeDashoffset = routeLength * (1 - progress);
    els.day.textContent = location.day;
    els.slot.textContent = location.slot;
    els.name.textContent = location.name;
    els.note.textContent = location.note;
    els.navLink.href = amapSearchUrl(location.navKeyword);
    els.navLink.setAttribute("aria-label", `在高德地图搜索 ${location.name}`);
    els.driveLabel.textContent = leg ? leg.label : "起点";
    els.driveCaption.textContent = leg ? leg.caption : "博山开局，路线开始。";
    els.legBadge.textContent = leg ? leg.label : itinerary.meta.disclaimer;

    renderDishes(location);
    syncTimelineActive();
  }

  function setActive(index) {
    activeIndex = (index + locations.length) % locations.length;
    renderActive();
  }

  function nextStation() {
    if (activeIndex === locations.length - 1) {
      pauseAutoplay();
      return;
    }
    setActive(activeIndex + 1);
  }

  function prevStation() {
    setActive(activeIndex - 1);
  }

  function startAutoplay() {
    playing = true;
    els.play.textContent = "Ⅱ";
    els.play.setAttribute("aria-label", "暂停");
    window.clearInterval(timer);
    timer = window.setInterval(nextStation, autoplayMs);
  }

  function pauseAutoplay() {
    playing = false;
    els.play.textContent = "▶";
    els.play.setAttribute("aria-label", "播放");
    window.clearInterval(timer);
  }

  function toggleAutoplay() {
    if (playing) {
      pauseAutoplay();
      return;
    }

    if (activeIndex === locations.length - 1) {
      setActive(0);
    }
    startAutoplay();
  }

  function renderTimeline() {
    els.timeline.innerHTML = "";
    locations.forEach((location, index) => {
      const leg = getInboundLeg(index);
      const item = document.createElement("li");
      item.className = "timeline-item";
      item.dataset.index = index;
      item.innerHTML = `
        <span class="timeline-index">${index + 1}</span>
        <div class="timeline-body">
          <div class="timeline-top">
            <span>${location.day}</span>
            <span>${location.slot}</span>
            <span>${location.area}</span>
          </div>
          <h3 class="timeline-title">${location.name}</h3>
          <p class="timeline-note">${location.dishes.length ? location.dishes.join(" / ") : location.note}</p>
          <p class="timeline-leg">${leg ? leg.label : "起点"} ${leg ? "· " + leg.caption : ""}</p>
        </div>
      `;
      item.addEventListener("click", () => {
        setActive(index);
        pauseAutoplay();
        window.scrollTo({ top: 0, behavior: "smooth" });
      });
      els.timeline.appendChild(item);
    });
  }

  function syncTimelineActive() {
    const items = els.timeline.querySelectorAll(".timeline-item");
    items.forEach((item, index) => {
      item.classList.toggle("is-active", index === activeIndex);
    });
  }

  function renderOptional() {
    const optional = itinerary.optional[0];
    if (!optional) {
      els.optionalBlock.hidden = true;
      return;
    }

    els.optionalBlock.innerHTML = `
      <h3>${optional.label}：${optional.name}</h3>
      <p>${optional.note}</p>
    `;
  }

  function bindControls() {
    els.prev.addEventListener("click", () => {
      prevStation();
      pauseAutoplay();
    });

    els.next.addEventListener("click", () => {
      if (activeIndex === locations.length - 1) {
        setActive(0);
      } else {
        setActive(activeIndex + 1);
      }
      pauseAutoplay();
    });

    els.play.addEventListener("click", toggleAutoplay);
    document.addEventListener("visibilitychange", () => {
      if (document.hidden) {
        window.clearInterval(timer);
      } else if (playing) {
        startAutoplay();
      }
    });
  }

  function init() {
    renderMarkers();
    buildRoutePath();
    renderTimeline();
    renderOptional();
    bindControls();
    renderActive();
    startAutoplay();
  }

  init();
})();
