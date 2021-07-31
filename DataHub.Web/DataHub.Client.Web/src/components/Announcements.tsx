import { observer } from 'mobx-react';
import { toast } from 'react-toastify';

import { AnnouncementEntity, useAnnouncementStore } from '../stores';

const Announcements = observer(() => {
  const { announcements } = useAnnouncementStore();
  
  announcements.map((announcement:AnnouncementEntity)=> toast.info(renderHtmlMessage(announcement.message), {
    autoClose: false,
    position: toast.POSITION.TOP_CENTER
  }));
  return null;
});

// See https://stackoverflow.com/questions/822452/strip-html-from-text-javascript/47140708#47140708
const renderHtmlMessage = (message: string) => {
  let doc = new DOMParser().parseFromString(message, 'text/html');
  return doc.body.textContent || "";
};


export default Announcements;
